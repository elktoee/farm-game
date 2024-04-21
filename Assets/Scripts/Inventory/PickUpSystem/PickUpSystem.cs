using Inventory.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField]
    private InventorySO inventoryData;

    // Подія, яка спрацьовує при підборі предмету 
    public event System.Action<ItemP> OnItemPickedUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemP item = collision.GetComponent<ItemP>();
        if (item != null)
        {
            int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
            if (reminder == 0)
                item.DestroyItem();
            else
                item.Quantity = reminder;
            OnItemPickedUp?.Invoke(item);
        }
    }
}