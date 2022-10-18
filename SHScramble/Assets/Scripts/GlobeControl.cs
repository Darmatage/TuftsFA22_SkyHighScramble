using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GlobeControl : MonoBehaviour
{
    public GameObject plane;
    public GameObject[] levels;
    public int mult = 3;
    public LevelHandler handle;
    private int unlockedlev;
    
    
    // Start is called before the first frame update
    void Start()
    {
        unlockedlev = handle.curlev;
        for (int i = 0; i < unlockedlev; i++)
        {
            levels[i].SetActive(true);
            levels[i].transform.GetChild(3).GetComponent<TextMeshPro>().text = "1-" + (i + 1);
        }
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
