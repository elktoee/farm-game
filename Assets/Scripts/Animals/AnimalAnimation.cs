using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalAnimation : MonoBehaviour
{
    public Patrol patrol;
    private Animator animator;
    private const string _x = "x";
    private const string _LastX = "LastX";



    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        animator.SetFloat(_x, patrol.moveVec.x);
        if(patrol.moveVec != Vector2.zero){
            animator.SetFloat(_LastX, patrol.moveVec.x);
        }
    }
}
