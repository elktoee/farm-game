using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBalance : MonoBehaviour
{
    private int coins = 100; // Початковий баланс гравця

    // Додаємо монети до балансу
    public void AddCoins(int amount)
    {
        coins += amount;
    }

    // Видаляємо монети з балансу
    public void RemoveCoins(int amount)
    {
        coins -= amount;
        if (coins < 0) // Перевіряємо, чи баланс не від'ємний
        {
            coins = 0; // Якщо так, встановлюємо баланс на 0
        }
    }

    // Отримуємо поточний баланс гравця
    public int GetCoins()
    {
        return coins;
    }
}
