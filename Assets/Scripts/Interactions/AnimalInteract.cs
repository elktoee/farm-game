using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalInteract : Interactable
{
    [SerializeField]
    private LootTable lootTable;

    [SerializeField]
    public int inv = 0;

    [SerializeField]
    private GameObject prefab;


    [SerializeField]
    private Sprite lootIcon;

    private Sprite oldIcon;

    private void Start(){
        oldIcon = Icon;
    }

     public override void Interact(GameObject player){
        if(inv == 0){
            if (lootTable != null)
        {
            List<GameObject> droppedItems = lootTable.GetRandomItems(UnityEngine.Random.Range(1, 4)); // Random between 1 and 3 items
            foreach (var itemPrefab in droppedItems)
            {
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
        }
            Destroy(transform.parent.gameObject);
        }
        else if(inv == 1){
            
            Instantiate(prefab, transform.position, Quaternion.identity);
            
            onLooted?.Invoke();
        
        }
    }

    public event Action onLooted;

    public void SetOldIcon(){
        Icon = oldIcon;
    }

    public void SetLootIcon(){
        Icon = lootIcon;
    }
}

