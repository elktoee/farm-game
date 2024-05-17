using System;
using UnityEngine;
using UnityEngine.Rendering;

public class DayPhaseController : MonoBehaviour
{
    [SerializeField]
    private Timer timer;
    [SerializeField]
    private Volume ppv;
    private bool _activateLights = true;
    [SerializeField]
    private GameObject[] _lights;

    public void setDay(bool value){
        if (value){
            ppv.weight = 0;
        }
        
    }

    private void Start(){
        timer.OnTimeChanged += OnTimeChanged;
    }

    private void OnTimeChanged(object sender, EventArgs e)
    {
        if (timer.Time > 5 * 60 && timer.Time < 8 * 60){ //dawning
            HandleDawning();
        }
        else if ( timer.Time >= 8 * 60 && timer.Time < 18 * 60){ //day
            ppv.weight = 0;
            if(_activateLights) {
                foreach (var light in _lights) light.SetActive(false);
                _activateLights = false;
            }
        }
        else if (timer.Time >= 18 * 60 && timer.Time < 22 * 60) //twilight
        {
            HandleTwilight();
        }
        else{ //night
            ppv.weight = 1;
            if(!_activateLights) {
                foreach (var light in _lights) light.SetActive(true);
                _activateLights = true;
            }
        }
    }

    private void Update()
    {
         if (timer.Time > 5 * 60 && timer.Time < 8 * 60){ //dawning
            HandleDawning();
        }
        else if ( timer.Time >= 8 * 60 && timer.Time < 18 * 60){ //day
            ppv.weight = 0;
        
        }
        else if (timer.Time >= 18 * 60 && timer.Time < 22 * 60) //twilight
        {
            HandleTwilight();
        }
        else{ //night
            ppv.weight = 1;

        }
    }

    private void HandleTwilight()
    {
        ppv.weight = Mathf.Lerp(0, 1, (timer.Time - 18 * 60) / 240);
        if(!_activateLights && timer.Time > 20*60) {
                foreach (var light in _lights) light.SetActive(true);
                _activateLights = true;
        }
        
    }

    private void HandleDawning()
    {
        ppv.weight = Mathf.Lerp(1, 0, (timer.Time - 5 * 60) / 180);
        if(_activateLights && timer.Time > 5.5f*60) {
                foreach (var light in _lights) light.SetActive(false);
                _activateLights = false;
        }
    }
}
