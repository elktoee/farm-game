using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class CollectQuestStep : QuestStep
{
    [System.Serializable]
    public class CollectQuestItem
    {
        public ItemSO itemSO;
        public int itemsToCollect;
        public int collectedItems;
        public bool isCollected;
    }

    [SerializeField]
    private InventorySO playerInventory;

    public List<CollectQuestItem> collectQuestItems = new List<CollectQuestItem>();

    private void Start()
    {
        // Subscribe to the item pickup event
        FindObjectOfType<PickUpSystem>().OnItemPickedUp += UpdateItemCount;

        // Check the current counts of collectible items
        foreach (var item in collectQuestItems)
        {
            ItemCountInInventory(item);
        }
    }

    private void UpdateItemCount(ItemP pickedItem)
    {
        // Update item counts when an item is picked up
        foreach (var item in collectQuestItems)
        {
            ItemCountInInventory(item);
        }

        // Check if all collections are completed
        CheckAllCollectionsCompleted();
    }

    private void ItemCountInInventory(CollectQuestItem questItem)
    {
        // Get the current state of the player's inventory
        Dictionary<int, InventoryItem> inventoryState = playerInventory.GetCurrentInventoryState();

        // Count the number of collected items
        questItem.collectedItems = 0;
        foreach (var kvp in inventoryState)
        {
            if (kvp.Value.item == questItem.itemSO)
            {
                questItem.collectedItems += kvp.Value.quantity;
            }
        }

        // Check if the quest item is collected
        if (questItem.collectedItems >= questItem.itemsToCollect)
        {
            questItem.collectedItems = questItem.itemsToCollect;
            questItem.isCollected = true;
        }
        else
        {
            questItem.isCollected = false;
        }
        UpdateState();
    }

    private void CheckAllCollectionsCompleted()
    {
        foreach (var item in collectQuestItems)
        {
            if (!item.isCollected)
            {
                // If any item is not collected, return without finishing the quest step
                return;
            }
        }

        // If all items are collected, finish the quest step
        FinishQuestStep();
    }

    private void UpdateState()
    {
        // Prepare strings to represent the state and status of all collected items
        string state = "";
        string status = "";

        // Iterate through each collectible item
        foreach (var item in collectQuestItems)
        {
            // Append the count of collected items for each item and a newline character
            state += item.collectedItems.ToString() + ",";
            status += item.itemSO.Name + " " + item.collectedItems + "/" + item.itemsToCollect + "\n";
        }

        // Remove the trailing comma from the state string
        state = state.TrimEnd(',');

        // Update the quest step state with information about all collected items
        ChangeState(state, status);
    }

    protected override void SetQuestStepState(string state)
    {
        // Split the state string to extract counts for each collectible item
        string[] counts = state.Split(',');

        // Update the collectedItems field for each collectible item
        for (int i = 0; i < counts.Length; i++)
        {
            collectQuestItems[i].collectedItems = int.Parse(counts[i]);
        }

        // Update the UI or other systems with the new state
        UpdateState();
    }

    public void RemoveItems(){
        foreach(var item in collectQuestItems){
            RemoveItemsFromInventory(item.itemSO, item.itemsToCollect);
        }
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