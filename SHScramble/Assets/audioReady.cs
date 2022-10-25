using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioReady : MonoBehaviour
{
    [SerializeField] private GameObject GameHandler;
    // Start is called before the first frame update
    void Start()
    {
        if(GameHandler.GetComponent<GameHandler>().tutorial)
            this.gameObject.SetActive(false);
    }

}
