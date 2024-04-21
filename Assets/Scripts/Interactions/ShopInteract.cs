using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopInteract : Interactable
{
    [SerializeField]
    private ShopInventory shopInventory;

    public override void Interact(GameObject player)
    {
        shopInventory.Show();
    }
}
