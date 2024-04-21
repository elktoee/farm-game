using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class PredefinedAction
{
    public string actionKey;
    public float startTime;
    public float delay;
}

[Serializable]
public class PredefinedActionList{
    public List<PredefinedAction> actions;
}

public class TimedActionManager : MonoBehaviour
{
    [SerializeField]
    private Timer timer;
    public static TimedActionManager instance;

    // Predefined dictionary of actions with string keys
    private Dictionary<string, Action> predefinedActions = new Dictionary<string, Action>
    {
        { "Action1", () => Debug.Log("Action 1 executed.") },
        { "Action2", () => Debug.Log("Action 2 executed.") }
        // Add more predefined actions here as needed
    };

    // List to store predefined actions with their expiration times
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

    // Method to execute action after a delay
    public void ExecuteActionAfterDelay(string key, float delay)
    {
        if (predefinedActions.TryGetValue(key, out Action action))
        {
            StartCoroutine(InvokeActionAfterDelay(action, delay));


            var predefinedAction = new PredefinedAction();
            predefinedAction.actionKey = key;
            predefinedAction.startTime = timer.FullTime;
            predefinedAction.delay = delay;
            predefinedActionList.Add(predefinedAction);
            SaveActionsToPlayerPrefs();
        }
    }

    private System.Collections.IEnumerator InvokeActionAfterDelay(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    // Method to save actions and their expiration times to PlayerPrefs
    private void SaveActionsToPlayerPrefs()
    {
        predefinedActionListClass.actions = predefinedActionList;
        string json = JsonUtility.ToJson(predefinedActionListClass);
        PlayerPrefs.SetString("PredefinedActions", json);
        PlayerPrefs.Save();
    }

    // Method to load actions and their expiration times from PlayerPrefs
    private void LoadActionsFromPlayerPrefs()
{
    List<PredefinedAction> actionsToRemove = new List<PredefinedAction>();

    if (PlayerPrefs.HasKey("PredefinedActions"))
    {
        string json = PlayerPrefs.GetString("PredefinedActions");

        Debug.Log(json);
        
        predefinedActionList = JsonUtility.FromJson<PredefinedActionList>(json).actions;

        foreach (PredefinedAction action in predefinedActionList)
        {
            
            if (action.startTime + action.delay >= timer.FullTime)
            {   
                Debug.Log(action.startTime + "   f" + timer.FullTime);
                float delay = action.startTime + action.delay - timer.FullTime;
                StartCoroutine(InvokeActionAfterDelay(() => ExecuteActionAfterDelay(action.actionKey, delay), delay));
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

        this.SaveActionsToPlayerPrefs();
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



}
