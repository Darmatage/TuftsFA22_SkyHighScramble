using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class OrderSpawner : MonoBehaviour
{
    public GameObject[] orders;
    public ParticleSystem burst;
    public Transform spawnPoint;
    public GameObject order = null;
    private int rangeEnd;
    private GameObject orderDestroy;
    public GameObject exclaim;
    public Material[] mats;
    public Material[] hiMats;
    public Material defaultRef;
    public Material highlightRef;
    public string current;
   
    public int time;
    public float timer;
 
    public bool hasOrder = false;

    public GameHandler gameHandler; 
 




    // Start is called before the first frame update
    void Start()
    {
        //choose random Material
        int matNum = Random.Range(0, mats.Length);
        defaultRef = mats[matNum];
        highlightRef = hiMats[matNum];
        transform.GetChild(1).GetComponent<MeshRenderer>().material = defaultRef;


        timer = 25.0f;
        rangeEnd = orders.Length - 1;
        exclaim.GetComponent<Animator>().Play("ExclaimNull");
        current = "none";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(hasOrder)
        {
            if(timer <= 0) {
                Destroy(orderDestroy);
                hasOrder = false;
                current = "none";
                exclaim.GetComponent<Animator>().Play("ExclaimNull");

                gameHandler.FailedOrder();  
                gameHandler.numOrders--;
            }
            else
                timer -= 0.01f;
        }
       
    }
 
    public void spawnOrder(){
        current = "order";
        exclaim.GetComponent<Animator>().Play("ExclaimNull");

        int SOnum = Random.Range(0, orders.Length);
        order = orders[SOnum];
        orderDestroy = Instantiate(order, spawnPoint.position, Quaternion.identity);
    }

    public void spawnExclaim()
    {
        current = "!";
        hasOrder = true;
        exclaim.GetComponent<Animator>().Play("ExclaimMain");
    } 


    void OnCollisionEnter(Collision other) {
         if (other.gameObject.layer == LayerMask.NameToLayer("pickup")) {
            if (order == null) {
                Destroy(other.gameObject);
            }
            else {
                string tag = other.gameObject.tag;
                string orderName = order.gameObject.name;
                if (tag == orderName) {
                    Destroy(other.gameObject);
                    Destroy(orderDestroy);
                    burst.Play();

                    hasOrder = false;
                    current = "none";
                    gameHandler.GoodOrder(); //get points
                    gameHandler.numOrders--;
                } else if (tag != orderName) {
                    Destroy(other.gameObject);
                    timer -= 1f;
                }
            }
         }
     }
     
}

