using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private float speed;
    private float waitTime;
    [SerializeField] private float startWaitTime;
    [SerializeField] private Transform moveSpot;
    private int randomSpot;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float maxY;
    [SerializeField] private float minY;

    public Vector2 moveVec;


    void Start()
    {
        waitTime = startWaitTime;
        moveSpot.position = new Vector2(Random.Range(minX,maxX), Random.Range(minY, maxY));
        moveVec = transform.position - moveSpot.position;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position,
             moveSpot.position, speed * Time.deltaTime);  

        if(Vector2.Distance(transform.position, moveSpot.position)< 0.2f){
            if(waitTime <= 0){
                moveSpot.position = new Vector2(Random.Range(minX,maxX), Random.Range(minY, maxY));
                moveVec = transform.position - moveSpot.position;
                waitTime = startWaitTime;
            }else{
                waitTime -= Time.deltaTime;
                moveVec = Vector2.zero;
            }
        }  
    }
}
