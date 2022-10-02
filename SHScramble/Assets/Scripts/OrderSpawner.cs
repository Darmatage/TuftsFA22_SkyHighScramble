using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class OrderSpawner : MonoBehaviour
{
    public GameObject chips;
    public GameObject soda;
    public Transform spawnPoint;
    public GameObject order;
    private int rangeEnd;
    private GameObject orderDestroy;
   
    public GameObject barObj;
    public int time;
 
    public float spawnRangeStart = 20.0f;
    public float spawnRangeEnd= 100.0f;
    private float timeToSpawn;
    private float spawnTimer = 0f;
    private bool orderFinished = true;
 
    // Start is called before the first frame update
    void Start()
    {
    }
 
    void spawnOrder(){
        int SOnum = Random.Range(1,3);
        if (SOnum == 1) { order = chips; }
        else if (SOnum == 2) { order = soda; }
        orderDestroy = Instantiate(order, spawnPoint.position, Quaternion.identity);
    }
 
    void OnCollisionEnter(Collision other) {
         if (other.gameObject.layer == LayerMask.NameToLayer("pickup")) {
            if (other.gameObject.tag == "Chips" && order == chips) {
                Destroy(other.gameObject);
                Destroy(orderDestroy);
                LeanTween.pause(barObj);
                LeanTween.scaleX(barObj, .88f, 0);
                orderFinished = true;
                //get point
                } else if (other.gameObject.tag == "Soda" && order == soda) {
                    Destroy(other.gameObject);
                    Destroy(orderDestroy);
                    LeanTween.pause(barObj);
                    LeanTween.scaleX(barObj, .88f, 0);
                    orderFinished = true;
                    //get point
                } else if (other.gameObject.tag == "Chips" && order == soda) {
                    Destroy(other.gameObject);
                    LeanTween.scaleX(barObj, 0, (time / 3));
                } else if (other.gameObject.name == "Soda" && order == chips) {
                    Destroy(other.gameObject);
                    LeanTween.scaleX(barObj, 0, (time / 3));
                }
         }
     }

 
 
    // Update is called once per frame
    void Update()
    {
        timeToSpawn = Random.Range(spawnRangeStart, spawnRangeEnd);
        if (orderFinished) {
            spawnTimer += 0.01f;
            if (spawnTimer >= timeToSpawn){
                spawnOrder();
                LeanTween.scaleX(barObj, 0, time);
                orderFinished = false;
                spawnTimer = 0f;
            }
        } else if(barObj.gameObject.transform.localScale.x == 0) {
            //lose point
            Destroy(orderDestroy);
            orderFinished = true;
            LeanTween.pause(barObj);
            LeanTween.scaleX(barObj, .88f, 0);
        }
       
    }
     
}

