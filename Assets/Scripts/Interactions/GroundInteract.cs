using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using Unity.VisualScripting;
using UnityEngine;

public class GroundInteract : Interactable
{
   
    private int state = 0;

    [SerializeField]
    private Sprite toPlantIcon;
    [SerializeField]
    private Sprite waterSprite;

    public Collider2D collider;

    private void Start(){
        collider = gameObject.GetComponent<Collider2D>();
    }

    public int State { get => state; set {
        state = value;
        if (state == 0){
            GameObject parentObject = transform.parent.gameObject;
            Renderer parentRenderer = parentObject.GetComponent<Renderer>();
            parentRenderer.material.color = new Color32(255, 255, 255,255);
        }
     }}

    public override void Interact(GameObject player){


        if (state == 0 && GroundInteractStatic.instanse.isSelectedObject && 
        GroundInteractStatic.instanse.SelectedImage == waterSprite){
            GameObject parentObject = transform.parent.gameObject;

            Renderer parentRenderer = parentObject.GetComponent<Renderer>();

            parentRenderer.material.color = new Color32(55, 55, 55,255);
            state++;
            Icon = toPlantIcon;
        }
        else if (state == 1 && GroundInteractStatic.instanse.isSelectedObject){
            GroundInteractStatic.instanse.Remove1();
            Instantiate(GroundInteractStatic.instanse.selectedObject,this.transform);
            state++;
            collider.enabled = false;
        }
        

    }
}
