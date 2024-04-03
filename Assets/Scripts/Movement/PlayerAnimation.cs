using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public MovementJoystick movementJoystick;
    private Animator animator;
    private const string _x = "x";
    private const string _y = "y";
    private const string _LastX = "LastX";
    private const string _LastY = "LastY";



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetFloat(_x, movementJoystick.joystickVec.x);
        animator.SetFloat(_y,movementJoystick.joystickVec.y);
        if(movementJoystick.joystickVec != Vector2.zero){
            animator.SetFloat(_LastX, movementJoystick.joystickVec.x);
            animator.SetFloat(_LastY,movementJoystick.joystickVec.y);
        }
    }
}
