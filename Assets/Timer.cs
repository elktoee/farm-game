using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    private Text _counterText;
    [SerializeField]
    private Volume ppv;

    public float Seconds { get; private set; }
    public float Minutes { get; private set; }

    [SerializeField]
    private bool _activateLights;
    [SerializeField]
    private GameObject[] _lights;
    
    // Startis called before the first frame update
    void Start()
    {
        _counterText = GetComponent<Text>() as Text;
        
    }

    // Update is called once per frame
    void Update()
    {
        Minutes = (int)((Time.time / 60f) % 24);
        Seconds = Time.time % 60f;
        HandleCounter();

        HandleTwilight();

        HandleDawning();

       
    }

    private void HandleCounter()
    {
        _counterText.text = Minutes.ToString("00") + ":" + Seconds.ToString("00");
    }

    void HandleTwilight()
    {
        if (Minutes is >= 21 and < 22)
        {
            ppv.weight = (float)Seconds / 60;

            if (_activateLights == false)
            {
                if (Seconds > 45)
                {
                    foreach (var t in _lights)
                    {
                        t.SetActive(true);
                    }

                    _activateLights = true;
                }
            }
        }
    }

    void HandleDawning()
    {
        if (Minutes is >= 4 and < 7)
        {
            ppv.weight = 1 - ((float)Seconds+(Minutes % 4)*60) / 180;
            
            if (_activateLights == true)
            {
                if (Seconds > 45 && Minutes == 4)
                {
                    for (int i = 0; i < _lights.Length; i++)
                    {
                        _lights[i].SetActive(false);
                    }
                    _activateLights = false;
                }
            }
        }
    }
}
