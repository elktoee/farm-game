using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class PredefinedAction
{
    public string actionKey;
    public float startTime;
    public float delay;
    public string json;
}

[Serializable]
public class PredefinedActionList
{
    public List<PredefinedAction> actions;
}

public class TimedActionManager : MonoBehaviour
{
    [SerializeField]
    private Timer timer;
    public static TimedActionManager instance;

    private Dictionary<string, Action<string>> predefinedActions = new Dictionary<string, Action<string>>
    {
        { "Action1", (jsonData) => {
            Debug.Log($"Action 1 executed with data: {jsonData}") ;
        
        }},
        { "Action2", (jsonData) => Debug.Log($"Action 2 executed with data: {jsonData}") }
        // Add more predefined actions here as needed
    };

    private List<PredefinedAction> predefinedActionList = new List<PredefinedAction>();

    private PredefinedActionList predefinedActionListClass = new PredefinedActionList();

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // Ensure only one instance exists
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        // Load saved actions and their expiration times
        LoadActionsFromPlayerPrefs();
    }

    public void ExecuteActionAfterDelay(string key, string jsonData, float delay)
    {
        if (predefinedActions.TryGetValue(key, out Action<string> action))
        {
            StartCoroutine(InvokeActionAfterDelay(() => action(jsonData), delay));

            var predefinedAction = new PredefinedAction();
            predefinedAction.actionKey = key;
            predefinedAction.startTime = timer.FullTime;
            predefinedAction.delay = delay;
            predefinedAction.json=jsonData;
            predefinedActionList.Add(predefinedAction);
            SaveActionsToPlayerPrefs();
        }
    }

    private System.Collections.IEnumerator InvokeActionAfterDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }


    private void SaveActionsToPlayerPrefs()
    {
        predefinedActionListClass.actions = predefinedActionList;
        string json = JsonUtility.ToJson(predefinedActionListClass);
        PlayerPrefs.SetString("PredefinedActions", json);
        PlayerPrefs.Save();
    }

    private void LoadActionsFromPlayerPrefs()
    {
        List<PredefinedAction> actionsToRemove = new List<PredefinedAction>();

        if (PlayerPrefs.HasKey("PredefinedActions"))
        {
            string json = PlayerPrefs.GetString("PredefinedActions");
            predefinedActionList = JsonUtility.FromJson<PredefinedActionList>(json).actions;

            foreach (PredefinedAction action in predefinedActionList)
            {
                if (action.startTime + action.delay >= timer.FullTime)
                {
                    float delay = action.startTime + action.delay - timer.FullTime;
                    StartCoroutine(InvokeActionAfterDelay(() => ExecuteActionAfterDelay(action.actionKey, action.json, delay), delay));
                }
                else
                {
                    actionsToRemove.Add(action);
                }
            }

            // Remove expired actions outside the loop
            foreach (PredefinedAction actionToRemove in actionsToRemove)
            {
                predefinedActionList.Remove(actionToRemove);
            }

            SaveActionsToPlayerPrefs();
        }
    }

    public void ExecuteActionAtInterval(Action action, float interval)
    {
        StartCoroutine(InvokeActionAtInterval(action, interval));
    }

    private System.Collections.IEnumerator InvokeActionAtInterval(Action action, float interval)
    {
        while (true)
        {
            yield return new WaitForSeconds(interval);
            action?.Invoke();
        }
    }

    private void Start(){
        TimedActionManager.instance.ExecuteActionAfterDelay("Action1","J212342SONDATA123",10);
    }
}
