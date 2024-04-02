using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [SerializeField]
    private Camera minimapCamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ZoomOut(){
        if(minimapCamera.fieldOfView < 171) 
        minimapCamera.fieldOfView++;
    }
    public void ZoomIn(){
        if(minimapCamera.fieldOfView > 166) 
        minimapCamera.fieldOfView--;
    }
}
