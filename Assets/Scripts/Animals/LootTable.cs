using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootEntry
{
    public GameObject itemPrefab;
    public float dropChance;
}

public class LootTable : MonoBehaviour
{
    public List<LootEntry> lootEntries = new List<LootEntry>();

    public List<GameObject> GetRandomItems(int count)
    {
        List<GameObject> droppedItems = new List<GameObject>();

        for (int i = 0; i < count; i++)
        {
            float totalWeight = 0f;
            foreach (var entry in lootEntries)
            {
                totalWeight += entry.dropChance;
            }

            float randomWeight = Random.Range(0f, totalWeight);

            foreach (var entry in lootEntries)
            {
                if (randomWeight < entry.dropChance)
                {
                    droppedItems.Add(entry.itemPrefab);
                    break;
                }
                randomWeight -= entry.dropChance;
            }
        }

        return droppedItems;
    }
}

public class Enemy : MonoBehaviour
{
    public LootTable lootTable;

    private void OnDestroy()
    {
        if (lootTable != null)
        {
            List<GameObject> droppedItems = lootTable.GetRandomItems(Random.Range(1, 4)); // Random between 1 and 3 items
            foreach (var itemPrefab in droppedItems)
            {
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }
        }
    }
}

