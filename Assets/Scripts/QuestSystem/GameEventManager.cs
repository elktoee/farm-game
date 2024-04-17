using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameEventManager : MonoBehaviour
{
    public static GameEventManager instance {get; private set;}

    public QuestEvents questEvents;

    private void Awake()
    {
        if(instance != null){
            Debug.LogError("Found more than one Game Events Manager in the scene");
        }
        instance = this;

        questEvents = new QuestEvents();
    }
}
