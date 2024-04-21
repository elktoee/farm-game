using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[RequireComponent(typeof(DataController))]
public class Timer : MonoBehaviour
{

    public event EventHandler OnSecondPassed;
    public event EventHandler OnTimeChanged;

    private int dayCount = 0;
    private float time;

    public float Time { get => time; set{
        time = value;
        OnTimeChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public float FullTime{ get => time + dayCount * 24*60;}

    private void Awake()
    {
        StartCoroutine(SecondTimer());
        OnSecondPassed+=CheckForDayEnd;
        Time = DataController.GameData.Time;
        
    }

    private void CheckForDayEnd(object sender, EventArgs e)
    {
        if (time >= 24*60){
            dayCount++;
            time -= 24 * 60;
        }
    }

    void FixedUpdate()
    {
        time += UnityEngine.Time.deltaTime;
    }

     private IEnumerator SecondTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            OnSecondPassed?.Invoke(this, EventArgs.Empty);
        }
    }
}
