using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory.UI{
public class Item : MonoBehaviour
{

    [SerializeField]
    private Image itemImage;

    [SerializeField]
    private TMP_Text quantityTxt;


    public event Action<Item> OnItemClicked,
    OnItemDroppedOn, OnItemBeingDrag, OnItemEndDrag;

    private bool empty = true;


    public void Awake()
    {
        ResetData();
    }


    public void ResetData(){
        this.itemImage.gameObject.SetActive(false);
        empty = false;
    }

    public void SetData(Sprite sprite, int quantity){
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.quantityTxt.text = quantity + "";
        empty = false;
    }

    public void OnBeginDrag(){
        if(empty)
        return;
        OnItemBeingDrag?.Invoke(this);
    }

    public void OnDrop(){
        OnItemDroppedOn?.Invoke(this);
    }

    public void OnEndDrag(){
        OnItemEndDrag?.Invoke(this);
    }

    public void OnPointerClick(BaseEventData data){
        PointerEventData pointerData = (PointerEventData)data;
        OnItemClicked?.Invoke(this); 
    }
}
}