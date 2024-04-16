using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalInteract : Interactable
{
    [SerializeField]
    private LootTable lootTable;

    [SerializeField]
    private int inv = 0;

    [SerializeField]
    private GameObject prefab;

     public override void Interact(GameObject player){
        if(inv == 0){
            if (lootTable != null)
        {
            List<GameObject> droppedItems = lootTable.GetRandomItems(Random.Range(1, 4)); // Random between 1 and 3 items
            foreach (var itemPrefab in droppedItems)
            {
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
        }
            Destroy(transform.parent.gameObject);
        }
        else if(inv == 1){
            Instantiate(prefab, transform.position, Quaternion.identity);
        }
    }
}
