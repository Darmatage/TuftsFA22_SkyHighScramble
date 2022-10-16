using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeControl : MonoBehaviour
{
    //public Transform[] levels;
    public Vector3[] levelspot;
    public GameObject plane;
    public int mult = 3;
    //public Transform center;
    public int curlev = -1;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
            transform.Rotate(0f, 0f, 0.05f * mult, Space.Self);
        if (Input.GetKey(KeyCode.A))
            transform.Rotate(0f, 0f, -0.05f * mult, Space.Self);
        if (Input.GetKey(KeyCode.W))
            transform.Rotate(0f, -0.05f * mult, 0f, Space.Self);
        if (Input.GetKey(KeyCode.S))
            transform.Rotate(0f, 0.05f * mult, 0f, Space.Self);
        
    }
}
