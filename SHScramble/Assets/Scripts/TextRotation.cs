using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextRotation : MonoBehaviour
{
    public Transform cam;

    private GameController gameController; // #1: add variable to hold GameController
    public bool orderFinished;

       void Start () {                      
          if (GameObject.FindWithTag ("GameController") != null) { 
            gameController = GameObject.FindWithTag ("GameController").GetComponent<GameController>();
         }
      }

       void Update () {
            
       }

       void OnCollisionEnter(Collider other){
             if (orderFinished != true) {
                   GetComponent<Collider>().enabled = false;
                   GetComponent<AudioSource>().Play();
                   gameController.MinusScore (1);         
                   StartCoroutine(DestroyThis());
             }
       }

       
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(cam.transform);
    }

    IEnumerator DestroyThis(){
             yield return new WaitForSeconds(0.5f);
             Destroy(gameObject);
       }

}
