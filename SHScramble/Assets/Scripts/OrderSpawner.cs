using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class OrderSpawner : MonoBehaviour
{
    public GameObject[] orders;
    public Transform spawnPoint;
    public GameObject order;
    private int rangeEnd;
    private GameObject orderDestroy;
    public GameObject exclaim;
    public string current;
   
    public GameObject barObj;
    public int time;
 
    public float spawnRangeStart = 20.0f;
    public float spawnRangeEnd= 100.0f;
    private float timeToSpawn;
    private float spawnTimer = 0f;
    public bool orderFinished = true;
 
    // Start is called before the first frame update
    void Start()
    {
        rangeEnd = orders.Length - 1;
        exclaim.GetComponent<Animator>().Play("ExclaimNull");
        current = "none";
    }

    // Update is called once per frame
    void Update()
    {
        timeToSpawn = Random.Range(spawnRangeStart, spawnRangeEnd);
        if (orderFinished) {
            spawnTimer += 0.01f;
            if (spawnTimer >= timeToSpawn){
                spawnExclaim();
                spawnTimer = 0f;
            }
        } 
        
        else if(barObj.gameObject.transform.localScale.x == 0) {
            //lose point
            Destroy(orderDestroy);
            orderFinished = true;
            current = "none";
            LeanTween.pause(barObj);
            LeanTween.scaleX(barObj, .88f, 0);
        }
       
    }
 
    public void spawnOrder(){
        current = "order";
        exclaim.GetComponent<Animator>().Play("ExclaimNull");

        LeanTween.scaleX(barObj, 0, time);
        orderFinished = false;
        int SOnum = Random.Range(0, orders.Length);
        order = orders[SOnum];
        orderDestroy = Instantiate(order, spawnPoint.position, Quaternion.identity);
    }

    void spawnExclaim()
    {
        current = "!";
        exclaim.GetComponent<Animator>().Play("ExclaimMain");
    }
 
    void OnCollisionEnter(Collision other) {
         if (other.gameObject.layer == LayerMask.NameToLayer("pickup")) {
            if (order == null) {
                Destroy(other.gameObject);
                current = "none";
            }
            else {
                string tag = other.gameObject.tag;
                string orderName = order.gameObject.name;
                if (tag == orderName) {
                    Destroy(other.gameObject);
                    Destroy(orderDestroy);
                    LeanTween.pause(barObj);
                    LeanTween.scaleX(barObj, .88f, 0);
                    orderFinished = true;
                    current = "none";
                    //get point
                } else if (tag != orderName) {
                    Destroy(other.gameObject);
                    current = "none";
                    LeanTween.scaleX(barObj, 0, (time / 3));
                }
            }
         }
     }  
     
}

