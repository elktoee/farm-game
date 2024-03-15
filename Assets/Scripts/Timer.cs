using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public event EventHandler OnSecondPassed;


    private int dayCount = 0;
    public float Time { get; private set; } = 0;
    public float ElapsedTime { get => Time - dayCount * 24 * 60;}

    private void Start()
    {
        StartCoroutine(SecondTimer());
        OnSecondPassed+=CheckForDayEnd;
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
