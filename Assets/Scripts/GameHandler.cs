using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public Transform playerTransform;
   

    private void Start()
    {
        cameraFollow.Setup(() => playerTransform.position);
    }

  
}
 