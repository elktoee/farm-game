using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewDoor :  Interactable
{
    [SerializeField]
    private Vector3 position;
    [SerializeField]
    private bool isOutside = true;
    [SerializeField]
    DayPhaseController dayPhaseController;

    public override void Interact(GameObject player){
        DataController.SaveGameData();
        player.transform.position = position;

        if (isOutside){
            dayPhaseController.enabled = true;
        }
        else{
            dayPhaseController.setDay(true);
            dayPhaseController.enabled = false;
        }
    }
}
