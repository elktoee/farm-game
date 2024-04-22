using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using Inventory.UI;
using UnityEngine;


public class GroundInteractStatic : MonoBehaviour{

    [SerializeField]
    private InventorySO inventorySO;

    [SerializeField]
    private UIInventory uIInventory;

    private void Start(){
        instanse = this; 
        uIInventory.OnDescriptionRequesed  += OnDescriptionRequesed;
    }

    public int currentId;

    private void OnDescriptionRequesed(int id)
    {
        currentId = id;
    }

    private Sprite selectedImage;

    public List<Sprite> images;
    public List<GameObject> objects;

    public bool isSelectedObject;

    public GameObject selectedObject;

    public static GroundInteractStatic instanse;

    public Sprite SelectedImage {
    get => selectedImage; 
    set {
        selectedImage = value;
        if (selectedImage != null && images.Contains(selectedImage)) {
            int index = images.IndexOf(selectedImage);
            if (index >= 0 && index < objects.Count) {
                selectedObject = objects[index];
                isSelectedObject = true;
            }
            else {
                isSelectedObject = false;
            }

        }  
        else isSelectedObject = false;
    } 
    }

    public void Remove1(){
        inventorySO.RemoveItem(currentId, 1);
    }
}
