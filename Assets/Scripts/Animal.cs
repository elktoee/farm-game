using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal : MonoBehaviour
{
    public float nextStateChance = 0.1f; // Шанс переходу до наступного стану кожен апдейт

    private int currentStateIndex = 0; // Поточний індекс стану рослини

    private bool growthCompleted = false; // Прапорець, що позначає завершення росту

    

    [SerializeField]
    private AnimalInteract animalInteract;

    void Start()
    {
        animalInteract.onLooted+=OnLoot;
        
    }

    private void OnLoot()
    {
        currentStateIndex = 0;
        growthCompleted = false;
        animalInteract.inv = 0;
        animalInteract.SetOldIcon();
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
        if (Random.value < nextStateChance * Time.deltaTime)
        {
            TransitionToNextState();
        }
    }

    void TransitionToNextState()
    {
        currentStateIndex++;
        if (currentStateIndex >= 2)
        {
            currentStateIndex = 2 - 1;
            growthCompleted = true;
            animalInteract.inv = 1;
            animalInteract.SetLootIcon();
        }
    }
}
