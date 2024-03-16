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


    private void Start(){
        

        timer.OnTimeChanged += OnTimeChanged;
    }

    private void OnTimeChanged(object sender, EventArgs e)
    {
        if (timer.ElapsedTime > 5 * 60 && timer.ElapsedTime < 8 * 60){ //dawning
            HandleDawning();
        }
        else if ( timer.ElapsedTime >= 8 * 60 && timer.ElapsedTime < 18 * 60){ //day
            ppv.weight = 0;
            if(_activateLights) {
                foreach (var light in _lights) light.SetActive(false);
                _activateLights = false;
            }
        }
        else if (timer.ElapsedTime >= 18 * 60 && timer.ElapsedTime < 22 * 60) //twilight
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
    }

    private void HandleTwilight()
    {
        ppv.weight = Mathf.Lerp(0, 1, (timer.ElapsedTime - 18 * 60) / 240);
        if(!_activateLights && timer.ElapsedTime > 20*60) {
                foreach (var light in _lights) light.SetActive(true);
                _activateLights = true;
        }
        
    }

    private void HandleDawning()
    {
        ppv.weight = Mathf.Lerp(1, 0, (timer.ElapsedTime - 5 * 60) / 180);
        if(_activateLights && timer.ElapsedTime > 5.5f*60) {
                foreach (var light in _lights) light.SetActive(false);
                _activateLights = false;
        }
    }
}
