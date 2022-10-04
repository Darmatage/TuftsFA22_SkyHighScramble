using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRotation : MonoBehaviour
{
    public Transform cam;


       void Start () {                      
          
      }
       
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(cam.transform);
    }

}
