using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using Inventory.UI;
using UnityEngine;

namespace Inventory{
public class Inventory : MonoBehaviour 
{
   [SerializeField] 
   private UIInventory inventoryUI;

   [SerializeField]
   private InventorySO inventoryData;

   public List<InventoryItem> initialItems = new List<InventoryItem>();

   private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
            foreach (var item in inventoryData.GetCurrentInventoryState())
            {
                inventoryUI.UpdateData(item.Key,
                   item.Value.item.ItemImage,
                   item.Value.quantity);
            }
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems){
               if(item.IsEmpty)
                  continue;
               inventoryData.AddItem(item);   
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState){
               inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, item.Value.quantity);
            }
        }

        private void PrepareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        this.inventoryUI.OnSwapItems += HandleSwapItems;
        this.inventoryUI.OnStartDragging += HandleDragging;
        this.inventoryUI.OnItemActionRequested += HandleItemActionRequest;
    }

    private void HandleItemActionRequest(int itemIndex)
    {

    }

    private void HandleDragging(int itemIndex)
    {
      InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
      if(inventoryItem.IsEmpty)
         return;
      inventoryUI.CreateDraggedItem(inventoryItem.item.ItemImage, inventoryItem.quantity);
      
    }

    private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
    {
      inventoryData.SwapItems(itemIndex_1, itemIndex_2);
    }

    
}
}