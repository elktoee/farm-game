using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestInteract : Interactable
{
    [Header("Quest")]
    [SerializeField] private QuestInfoSO questInfoForPoint;

    [Header("Config")]
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool finishPoint = true;

    [SerializeField] private QuestUI questUI;

    private string questId;
    private QuestState currentQuestState;

    private QuestIcon questIcon;


    private void Awake() 
    {
        questId = questInfoForPoint.id;
        questIcon = GetComponentInChildren<QuestIcon>();
    }

    private void OnEnable()
    {
        GameEventManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }

    private void QuestStateChange(Quest quest)
    {
        // only update the quest state if this point has the corresponding quest
        if (quest.info.id.Equals(questId))
        {
            currentQuestState = quest.state;
            questIcon.SetState(currentQuestState, startPoint, finishPoint);
        }
    }

    public override void Interact(GameObject player)
    {
         if (currentQuestState.Equals(QuestState.CAN_START) && startPoint)
        {
            questUI.Show(questInfoForPoint);
        }
        else if (currentQuestState.Equals(QuestState.CAN_FINISH) && finishPoint)
        {
            GameEventManager.instance.questEvents.FinishQuest(questId);
            
        }
        
    }

    public void QuestStart(){
        
        GameEventManager.instance.questEvents.StartQuest(questId);
        questUI.Hide();
       
    }

}
