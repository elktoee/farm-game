using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public event EventHandler OnSecondPassed;
    public event EventHandler OnTimeChanged;

    private int dayCount = 0;
    private float time = 0;

    public float Time { get => time; set{
        time = value;
        OnTimeChanged.Invoke(this, EventArgs.Empty);
        }
    }
    public float ElapsedTime { get => Time - dayCount * 24 * 60;}

    private void Start()
    {
        Time = 0;
        StartCoroutine(SecondTimer());
        OnSecondPassed+=CheckForDayEnd;
        Time = 15*60;
    }

    private void CheckForDayEnd(object sender, EventArgs e)
    {
        if (ElapsedTime >= 24*60){
            dayCount++;
        }
    }

    void FixedUpdate()
    {
        Time += UnityEngine.Time.deltaTime;
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
