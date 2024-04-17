using UnityEngine;
using TMPro;

public class DisplayBalanceUI : MonoBehaviour
{
    public TMP_Text balanceText; // Посилання на текстове поле для відображення балансу
    public PlayerBalance playerBalance; // Посилання на клас управління балансом гравця

    private void Update()
    {
        // Оновлюємо текстове поле з балансом гравця
        UpdateBalanceUI();
    }

    // Метод для оновлення текстового поля з балансом гравця
    private void UpdateBalanceUI()
    {
        // Отримуємо поточний баланс гравця
        int currentBalance = playerBalance.GetCoins();

        // Оновлюємо текстове поле з балансом гравця
        balanceText.text = currentBalance.ToString();
    }
}