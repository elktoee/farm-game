using UnityEngine;
using System.Collections.Generic;

public class Plant : MonoBehaviour
{
    public List<Sprite> sprites; // Список спрайтів рослини для кожного стану
    public float nextStateChance = 0.1f; // Шанс переходу до наступного стану кожен апдейт

    private int currentStateIndex = 0; // Поточний індекс стану рослини
    private SpriteRenderer spriteRenderer; // Компонент відображення спрайта
    private  Collider2D plantCollider;
    private bool growthCompleted = false; // Прапорець, що позначає завершення росту

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        plantCollider = GetComponent<Collider2D>();
        // Встановити спрайт рослини для початкового стану
        spriteRenderer.sprite = sprites[currentStateIndex];
    }

    void Update()
    {
        if (!growthCompleted)
        {
            TryTransitionToNextState();
        }
    }

    void TryTransitionToNextState()
    {
        // Перевіряємо, чи рослина може перейти в наступний стан
        if (Random.value < nextStateChance)
        {
            TransitionToNextState();
        }
    }

    void TransitionToNextState()
    {
        // Логіка переходу до наступного стану
        currentStateIndex++;
        if (currentStateIndex >= sprites.Count)
        {
            currentStateIndex = sprites.Count - 1;
            growthCompleted = true; // Позначаємо, що ріст завершено
            Debug.Log("Plant growth completed.");
            plantCollider.enabled = true; // Перемикаємо колайдер на ввімкнено
        }
        spriteRenderer.sprite = sprites[currentStateIndex];
        Debug.Log("The plant is now in state: " + currentStateIndex);
    }
}