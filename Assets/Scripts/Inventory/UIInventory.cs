using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;


namespace Inventory.UI{
public class UIInventory : MonoBehaviour
{
    [SerializeField]
    private Item itemPrefab;

    [SerializeField]
    private RectTransform contentPanel;

    [SerializeField]
    private MouseFollower mouseFollower;

    List<Item> ListOfUIItems = new List<Item>();

    private int currentlyDraggedItemIndex = -1;

    public event Action<int> OnDescriptionRequesed, 
        OnItemActionRequested,
        OnStartDragging;
    public event Action<int, int> OnSwapItems;

    private void Awake()
    {
        mouseFollower.Toggle(false);
    }

    public void InitializeInventoryUI(int inventorysize)
    {
        for (int i = 0; i < inventorysize; i++)
        {
            Item uiItem =
                Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            ListOfUIItems.Add(uiItem);
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemBeingDrag += HandleBeginDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnItemEndDrag += HandleEndDrag;
        }
    }

    public void UpdateData(int itemIndex,
        Sprite itemImage, int itemQuantity)
    {
        if(ListOfUIItems.Count > itemIndex){
            ListOfUIItems[itemIndex].SetData(itemImage, itemQuantity);
        }
    }

    private void HandleEndDrag(Item item)
    {
        ResetDraggtedItem();
    }

    private void HandleSwap(Item item)
    {
        int index = ListOfUIItems.IndexOf(item);
        if(index == -1)
        {
            return;
        }
        OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
    }

    private void ResetDraggtedItem()
    {
        mouseFollower.Toggle(false);
        currentlyDraggedItemIndex = -1;
    }

    private void HandleBeginDrag(Item item)
    {
        int index = ListOfUIItems.IndexOf(item);
        if(index == -1)
            return;
        currentlyDraggedItemIndex = index;
        HandleItemSelection(item);
        OnStartDragging?.Invoke(index);
    }

    public void CreateDraggedItem(Sprite sprite, int quantity){
        mouseFollower.Toggle(true);
        mouseFollower.SetData(sprite, quantity);
    }

    private void HandleItemSelection(Item item)
    {
        int index = ListOfUIItems.IndexOf(item);
        if(index == -1)
            return;
        OnDescriptionRequesed?.Invoke(index);
    }

        internal void ResetAllItems()
        {
            foreach (var item in ListOfUIItems){
                item.ResetData();
            }
        }

        
    }
}