using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundInteract : Interactable
{
    public int var;

    
    
    public override void Interact(GameObject player){
        if (var == 0){
            GameObject parentObject = transform.parent.gameObject;

            Renderer parentRenderer = parentObject.GetComponent<Renderer>();

            parentRenderer.material.color = new Color32(55, 55, 55,255);
        }
    }
}
