using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementJoystick : MonoBehaviour
{
    public GameObject joystick;
    public GameObject joystickBG;
    public Vector2 joystickVec;
    public Vector2 joystickTouchPos;
    private float joystickRadius;
    private Vector2 joystickOriginalPos;
    
    // Start is called before the first frame update
    void Start()
    {
        joystickRadius = joystickBG.GetComponent<RectTransform>().sizeDelta.y / 4;
        joystick.SetActive(false);
        joystickBG.SetActive(false);
        
    }

    public void PointerDown()
    {
        joystick.SetActive(true);
        joystickBG.SetActive(true);
        joystick.transform.position = Input.mousePosition;
        joystickBG.transform.position = Input.mousePosition;
        joystickTouchPos = Input.mousePosition;
    }

    public void Drag(BaseEventData baseEventData)
    {
        PointerEventData pointerEventData = baseEventData as PointerEventData;
        Vector2 dragPos = pointerEventData.position;
        joystickVec = (dragPos - joystickTouchPos).normalized;

        float joystickDist = Vector2.Distance(dragPos, joystickTouchPos);
        if (joystickDist < joystickRadius)
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickDist;
            
        }

        else
        {
            joystick.transform.position = joystickTouchPos + joystickVec * joystickRadius;
        }
    }

    public void Hide()
    {
        joystick.SetActive(false);
        joystickBG.SetActive(false);
        joystickVec = Vector2.zero;
    }
}