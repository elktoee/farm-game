using UnityEngine;
using System;
using System.Collections.Generic;

public class TimedActionManager : MonoBehaviour
{
    [SerializeField]
    private Timer timer;
    private const string ActionDataKey = "ActionData";

    [Serializable]
    public class ActionData
    {
        public string SerializedDelegate;
        public string ParameterBase64;
        public string ParameterType;
        public float Delay;
        public float StartTime;

        public ActionData(string serializedDelegate, string parameterBase64, string parameterType, float delay, float startTime)
        {
            SerializedDelegate = serializedDelegate;
            ParameterBase64 = parameterBase64;
            ParameterType = parameterType;
            Delay = delay;
            StartTime = startTime;
        }
    }

    private void Start()
    {
        // Deserialize and execute stored actions on start
        DeserializeAndExecuteStoredActions();
    }

    public void ExecuteActionAfterDelay<T>((Action<T> action, T parameter, float delay) actionInfo)
    {
        ActionData data = new ActionData(
            SerializeDelegateSystem.SerializeDelegate(actionInfo.action),
            Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(actionInfo.parameter))),
            typeof(T).AssemblyQualifiedName,
            actionInfo.delay,
            Timer.FullTime
        );

        // Retrieve existing action data from PlayerPrefs
        List<ActionData> actionDataList = GetStoredActionData();

        // Add new action data
        actionDataList.Add(data);

        // Store updated action data list in PlayerPrefs
        StoreActionData(actionDataList);

        StartCoroutine(ExecuteDelayedAction(actionInfo.action, actionInfo.parameter, actionInfo.delay));
    }

    private System.Collections.IEnumerator ExecuteDelayedAction<T>(Action<T> action, T parameter, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Execute the action
        action(parameter);
    }

    private List<ActionData> GetStoredActionData()
    {
        string storedDataJson = PlayerPrefs.GetString(ActionDataKey);
        return string.IsNullOrEmpty(storedDataJson) ? new List<ActionData>() : JsonUtility.FromJson<List<ActionData>>(storedDataJson);
    }

    private void StoreActionData(List<ActionData> actionDataList)
    {
        string dataJson = JsonUtility.ToJson(actionDataList);
        PlayerPrefs.SetString(ActionDataKey, dataJson);
    }

    private void DeserializeAndExecuteStoredActions()
    {
        List<ActionData> actionDataList = GetStoredActionData();
        foreach (var actionData in actionDataList)
        {
            // Deserialize the delegate
            Type delegateType = typeof(Action);
            Delegate deserializedDelegate = SerializeDelegateSystem.DeserializeDelegate(delegateType, actionData.SerializedDelegate);

            // Deserialize the parameter
            Type parameterType = Type.GetType(actionData.ParameterType);
            object parameter = DeserializeParameter(actionData.ParameterBase64, parameterType);

            // Calculate remaining delay
            float elapsedTime = timer.FullTime - actionData.StartTime;
            float remainingDelay = Mathf.Max(actionData.Delay - elapsedTime, 0f);

            // Execute the action after the remaining delay
            StartCoroutine(ExecuteDelayedAction(deserializedDelegate, parameter, remainingDelay));
        }
    }

    private object DeserializeParameter(string parameterBase64, Type type)
    {
        byte[] parameterBytes = Convert.FromBase64String(parameterBase64);
        string parameterJson = System.Text.Encoding.UTF8.GetString(parameterBytes);
        return JsonUtility.FromJson(parameterJson, type);
    }
}

public class SerializeDelegateSystem
{
    public static string SerializeDelegate(Delegate d)
    {
        using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            formatter.Serialize(stream, d);
            return Convert.ToBase64String(stream.ToArray());
        }
    }

    public static Delegate DeserializeDelegate(Type delegateType, string serializedDelegate)
    {
        byte[] bytes = Convert.FromBase64String(serializedDelegate);
        using (System.IO.MemoryStream stream = new System.IO.MemoryStream(bytes))
        {
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            return formatter.Deserialize(stream) as Delegate;
        }
    }
}
