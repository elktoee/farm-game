using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantInteract : Interactable
{
    [SerializeField]
    private LootTable lootTable;

    private GroundInteract parentCollider; // Колайдер батьківського об'єкта

    void Start()
    {
        parentCollider = GetComponentInParent<GroundInteract>(); // Отримати колайдер батьківського об'єкта
    }

    public override void Interact(GameObject player)
    {
        if (lootTable != null)
        {
            List<GameObject> droppedItems = lootTable.GetRandomItems(Random.Range(1, 4)); // Random between 1 and 3 items
            foreach (var itemPrefab in droppedItems)
            {
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
        }
        
        // Ввімкнути колайде рбатьківського об'єкта
        if (parentCollider != null)
        {
            parentCollider.collider.enabled = true;
            parentCollider.State = 0;
        }
        
        Destroy(this.gameObject);
    }
}