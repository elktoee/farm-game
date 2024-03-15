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

    private bool isTransitioning = false;

    private void Update()
    {
        if (timer.ElapsedTime > 5 * 60 && timer.ElapsedTime < 8 * 60)
        {
            HandleDawning();
            if(_activateLights && timer.ElapsedTime > 5.5f*60) {
                foreach (var light in _lights) light.SetActive(false);
                _activateLights = false;
            }
            
        }

        else if (timer.ElapsedTime > 18 * 60 && timer.ElapsedTime < 22 * 60)
        {
            HandleTwilight();
            if(!_activateLights && timer.ElapsedTime > 20*60) {
                foreach (var light in _lights) light.SetActive(true);
                _activateLights = true;
            }
        }
    }

    private void HandleTwilight()
    {
        if (!isTransitioning)
        {

            StartCoroutine(TransitionLight(240, ppv.weight, 1));
        }
    }

    private void HandleDawning()
    {
        if (!isTransitioning)
        {

            StartCoroutine(TransitionLight(180, ppv.weight, 0));
        }
    }

    private System.Collections.IEnumerator TransitionLight(float duration, float startWeight, float targetWeight)
    {
        isTransitioning = true;
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            ppv.weight = Mathf.Lerp(startWeight, targetWeight, elapsedTime / duration);
            yield return null;
        }

        ppv.weight = targetWeight;
        isTransitioning = false;
    }
}
