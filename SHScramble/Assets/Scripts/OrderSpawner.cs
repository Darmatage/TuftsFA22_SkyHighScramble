using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class OrderSpawner : MonoBehaviour
{
    public Sprite[] orders;
    public ParticleSystem burst;
    public ParticleSystem orderpart;
    public string order = "null";
    private int rangeEnd;
    public GameObject exclaim;
    public Material[] mats;
    public Material[] hiMats;
    public Material defaultRef;
    public Material highlightRef;
    public string current;
    public GameObject bg;
   
    public int time;
    public float timer;
    public GameObject sr;
    public GameObject item;
    public Sprite[] face;
 
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
        bg.SetActive(false);


        timer = 15.0f;
        rangeEnd = orders.Length - 1;
        exclaim.GetComponent<Animator>().Play("ExclaimNull");
        current = "none";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(hasOrder)
        {
            //Change Face
            if(timer <= 4.0f)
            {
                sr.GetComponent<SpriteRenderer>().sprite = face[2];
                sr.GetComponent<Animator>().SetFloat("Speed", 2.5f);
            }
            else if(timer <= 8.0f)
            {
                sr.GetComponent<SpriteRenderer>().sprite = face[1];
                sr.GetComponent<Animator>().SetFloat("Speed", 1.5f);
            }
            else
            {
                sr.GetComponent<SpriteRenderer>().sprite = face[0];
                sr.GetComponent<Animator>().SetFloat("Speed", 1.0f);
            }


            if(timer <= 0) {
                item.SetActive(false);
                hasOrder = false;
                current = "none";
                exclaim.GetComponent<Animator>().Play("ExclaimNull");
                orderpart.Stop();

                bg.SetActive(false);
                gameHandler.FailedOrder();  
                gameHandler.numOrders--;
            }
            else
                timer -= 0.01f;
        }
       
    }
 
    public void spawnOrder(){
        orderpart.Stop();
        current = "order";
        exclaim.GetComponent<Animator>().Play("ExclaimNull");

        int SOnum = Random.Range(0, orders.Length);
        item.GetComponent<SpriteRenderer>().sprite = orders[SOnum];
        order = item.GetComponent<SpriteRenderer>().sprite.name;
        bg.SetActive(true);
        item.SetActive(true);
    }

    public void spawnExclaim()
    {
        orderpart.Play();
        current = "!";
        hasOrder = true;
        exclaim.GetComponent<Animator>().Play("ExclaimMain");
    } 


    void OnCollisionEnter(Collision other) {
         if (other.gameObject.layer == LayerMask.NameToLayer("pickup")) {
            if (order == "null") {
                Destroy(other.gameObject);
            }
            else {
                string tag = other.gameObject.tag;
                if (tag == order) {
                    Destroy(other.gameObject);
                    item.SetActive(false);
                    bg.SetActive(false);
                    burst.Play();

                    hasOrder = false;
                    current = "none";
                    gameHandler.GoodOrder(); //get points
                    gameHandler.numOrders--;
                } else if (tag != order) {
                    Destroy(other.gameObject);
                    timer -= 1f;
                }
            }
         }
     }
     
}

