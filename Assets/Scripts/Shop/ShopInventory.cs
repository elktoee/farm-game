using System;
using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using Inventory.UI;
using Unity.VisualScripting;
using UnityEngine;

public class ShopInventory : MonoBehaviour
{

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private InventorySO inventoryData;

    [SerializeField]
    private SelectedItemInfoPanel selectedItemInfoPanel;

    [SerializeField] 
    private UIInventory inventoryUI;

    public ItemSO selectedItem;

    [SerializeField]
    private InventorySO playerInventory;

    [SerializeField]
    private PlayerBalance playerBalance;

    public bool shopPage = true;

    public List<InventoryItem> initialItems = new List<InventoryItem>();

    private void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        private void PrepareInventoryData()
        {
            inventoryData.Initialize();
            inventoryData.OnInventoryUpdated += UpdateInventoryUI;
            foreach (InventoryItem item in initialItems)
            {
                if (item.IsEmpty)
                    continue;
                inventoryData.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryUI.UpdateData(item.Key, item.Value.item.ItemImage, 
                    item.Value.quantity);
            }
        }

    private void Awake(){
        Hide();
        selectedItemInfoPanel.ResetDescription();
    }

    public void Hide(){
        panel.SetActive(false);
    }

    public void Show()
    {
        panel.SetActive(true);
            foreach (var item in inventoryData.GetCurrentInventoryState())
            {
                inventoryUI.UpdateData(item.Key,
                    item.Value.item.ItemImage,
                    item.Value.quantity);
            }
        selectedItemInfoPanel.ResetDescription();
    }

    public void ShowBuyPage()
    {
        inventoryUI.ResetAllItems();
            foreach (var item in inventoryData.GetCurrentInventoryState())
            {
                inventoryUI.UpdateData(item.Key,
                    item.Value.item.ItemImage,
                    item.Value.quantity);
            }
        selectedItemInfoPanel.ResetDescription();
        shopPage = true;
    }

    public void ShowSellPage()
    {
        inventoryUI.ResetAllItems();
            foreach (var item in playerInventory.GetCurrentInventoryState())
            {
                inventoryUI.UpdateData(item.Key,
                    item.Value.item.ItemImage,
                    item.Value.quantity);
            }
        selectedItemInfoPanel.ResetDescription();
        shopPage = false;
    }
    

    private void PrepareUI()
    {
        inventoryUI.InitializeInventoryUI(inventoryData.Size);
        this.inventoryUI.OnDescriptionRequesed += HandleIDescriptionRequest;
    }

    private void HandleIDescriptionRequest(int itemIndex)
    {
        if(shopPage){
        InventoryItem inventoryItem = inventoryData.GetItemAt(itemIndex);
        if(inventoryItem.IsEmpty)
            return;
        ItemSO item = inventoryItem.item;
        selectedItem = item;
        selectedItemInfoPanel.SetDescription(item.ItemImage, item.name, item.Description, item.Price);
        }

        else{
        InventoryItem inventoryItem = playerInventory.GetItemAt(itemIndex);
        if(inventoryItem.IsEmpty)
            return;
        ItemSO item = inventoryItem.item;
        selectedItem = item;
        selectedItemInfoPanel.SetDescription(item.ItemImage, item.name, item.Description, item.Price);
        }

    }
    
    public void PurchaseItem(){
        if (playerBalance.GetCoins() >= selectedItem.Price*selectedItemInfoPanel.amount) // Перевіряємо, чи гравець має достатньо коштів для покупки
        {
            // Видаляємо вартість товару з балансу гравця
            playerBalance.RemoveCoins(selectedItem.Price*selectedItemInfoPanel.amount);

            // Додаємо куплений товар до інвентаря гравця
            playerInventory.AddItem(selectedItem, selectedItemInfoPanel.amount);
        }

        selectedItemInfoPanel.ResetDescription();
    }

    public int GetItemCount(ItemSO item)
    {
        int itemCount = 0;

        foreach (var inventoryItem in playerInventory.GetCurrentInventoryState())
        {
            if (inventoryItem.Value.item == item)
            {
                itemCount += inventoryItem.Value.quantity;
            }
        }

        return itemCount;
    }

    public void SellItem(){
        // Видаляємо вартість товару з балансу гравця
        playerBalance.AddCoins(selectedItem.Price*selectedItemInfoPanel.amount);

        // Додаємо куплений товар до інвентаря гравця
        RemoveItemsFromInventory(selectedItem, selectedItemInfoPanel.amount);
        selectedItemInfoPanel.amount = 1;

        inventoryUI.ResetAllItems();
            foreach (var item in playerInventory.GetCurrentInventoryState())
            {
                inventoryUI.UpdateData(item.Key,
                    item.Value.item.ItemImage,
                    item.Value.quantity);
            }
        selectedItemInfoPanel.ResetDescription();
    }

    public void RemoveItemsFromInventory(ItemSO item, int itemsToRemove)
    {
        // Отримуємо поточний стан інвентаря гравця
        Dictionary<int, InventoryItem> inventoryState = playerInventory.GetCurrentInventoryState();

        // Переглядаємо усі предмети в інвентарі
        foreach (var kvp in inventoryState)
        {
            // Якщо це предмет, який ми хочемо видалити
            if (kvp.Value.item == item)
            {
                // Перевіряємо, скільки з нього ми хочемо видалити
                if (itemsToRemove >= kvp.Value.quantity)
                {
                    // Якщо ми хочемо видалити більше або стільки ж, скільки є в інвентарі, то просто видаляємо весь предмет
                    playerInventory.RemoveItem(kvp.Key, kvp.Value.quantity);
                    itemsToRemove -= kvp.Value.quantity;
                }
                else
                {
                    // Якщо ми хочемо видалити менше, ніж є в інвентарі, то просто видаляємо потрібну кількість
                    playerInventory.RemoveItem(kvp.Key, itemsToRemove);
                    break; // Вийдемо з циклу, бо вже видалили все, що хотіли
                }
            }
        }
    }

}