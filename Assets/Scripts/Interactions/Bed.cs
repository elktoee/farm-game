using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bed : Interactable
{
    [SerializeField] private GameObject movementJoystick;
    [SerializeField] private Timer timer;
    private Collider2D bedCollider;
    private bool isOnBed = false;

    void Start()
    {
        bedCollider = GetComponent<Collider2D>();
    }

    public override void Interact(GameObject player)
    {
        float currentTime = timer.Time;

        if ((currentTime >= 20 * 60 || currentTime < 6 * 60) && !isOnBed)
        {
            EnterBed(player);
        }
        else ExitBed();
    }

    private void EnterBed(GameObject player)
    {
        bedCollider.enabled = false;
        player.transform.position = transform.position;
        movementJoystick.SetActive(false);
        isOnBed = true;
        Time.timeScale = 50;
    }

    private void ExitBed()
    {
        bedCollider.enabled = true;
        movementJoystick.SetActive(true);
        isOnBed = false;
        Time.timeScale = 1;
    }


 void Update()
    {
        if ((timer.Time <= 20 * 60 && timer.Time > 6 * 60) && isOnBed)
        {
            ExitBed();
        }
    }
}
