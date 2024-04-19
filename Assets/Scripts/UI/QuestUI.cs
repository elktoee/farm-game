using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject questPanel;
    [SerializeField] private TextMeshProUGUI questName;
    [SerializeField] private TextMeshProUGUI questDescription;
    [SerializeField] private TextMeshProUGUI questCard;
    [SerializeField] private TextMeshProUGUI goldReward;

    public void Show(QuestInfoSO quest){
        questName.text = quest.displayName;
        questDescription.text = quest.description;
        questCard.text = quest.quest;
        goldReward.text = quest.goldReward + " Gold";

        questPanel.SetActive(true);
    }

    public void Hide(){
        questPanel.SetActive(false);
    }
}
