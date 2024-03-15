using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class CanvasTimeCounter : MonoBehaviour
{

    [SerializeField]
    private Timer timer;
    private TextMeshProUGUI _counterText;
    // Start is called before the first frame update
    void Start()
        {
            _counterText = GetComponent<TextMeshProUGUI>();
            timer.OnSecondPassed += HandleCounter;
        }

    private void HandleCounter(object sender, EventArgs e)
    {
        int Seconds = (int)(timer.Time % 60);
        int Minutes = (int)(timer.Time/60 % 24);
        _counterText.text = Minutes.ToString("00") + ":" + Seconds.ToString("00");
    }
}
