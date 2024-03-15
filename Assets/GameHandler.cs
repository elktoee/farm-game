using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public CameraFollow cameraFollow;
    public Transform playerTransform;
    public static WorldPrefs worldPrefs;


    private void Awake()
    {
        if (PlayerPrefs.HasKey("WorldPrefs")) {
            string serializedData = PlayerPrefs.GetString("WorldPrefs");
            worldPrefs = JsonUtility.FromJson<WorldPrefs>(serializedData);
        } else {
            worldPrefs = new WorldPrefs();
        }
    }

    private void Start()
    {
        
        
        cameraFollow.Setup(() => playerTransform.position);
    }

    public static void SaveTime(float time)
    {
        worldPrefs.Time = time;
        PlayerPrefs.SetString( "WorldPrefs", JsonUtility.ToJson(worldPrefs));
        PlayerPrefs.Save();
    }

    public static float getTime() => worldPrefs.Time;
}
 