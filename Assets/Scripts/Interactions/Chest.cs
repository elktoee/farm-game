using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    [SerializeField]
    private GameObject chestUI; 

 public override void Interact(GameObject player)
    {
        Show();
    }

    public void Hide(){
        chestUI.SetActive(false);
    }

    private void Show(){
        chestUI.SetActive(true);
    }
}
