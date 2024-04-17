using System.Collections;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class CollectQuestStep : QuestStep
{
    [SerializeField]
    private ItemSO itemSO;

    [SerializeField]
    private InventorySO playerInventory;

    private int itemsToComplete = 10;




    private void Start()
    {
        // Підписка на подію підбору предмету
        FindObjectOfType<PickUpSystem>().OnItemPickedUp += UpdateItemCount;

        // Перевірка кількості itemSO в інвентарі гравця
        ItemCountInInventory(itemSO);

    }

    // Метод для оновлення кількості itemSO в інвентарі гравця
    private void UpdateItemCount(ItemP pickedItem)
    {
         // Перевірка кількості itemSO в інвентарі гравця
        ItemCountInInventory(itemSO);

    }

    // Метод для отримання кількості itemSO в інвентарі гравця
    private void ItemCountInInventory(ItemSO item)
    {
        int count = 0;

        // Отримати поточний стан інвентаря гравця
        Dictionary<int, InventoryItem> inventoryState = playerInventory.GetCurrentInventoryState();

        // Підрахувати кількість itemSO в інвентарі
        foreach (var kvp in inventoryState)
        {
            if (kvp.Value.item == item)
            {
                count += kvp.Value.quantity;
            }
        }

        // Далі ви можете використати кількість itemCount за вашою потребою
        Debug.Log($"Кількість {itemSO.name} в інвентарі гравця: {count}");
        

        if(count >= itemsToComplete){
            FinishQuestStep();
        }

    }
}