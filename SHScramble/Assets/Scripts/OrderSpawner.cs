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
    public string order1 = "null";
    public string order2 = "null";
    private int rangeEnd;
    public GameObject exclaim;
    public GameObject happy;
    public Material[] mats;
    public Material[] hiMats;
    public Material defaultRef;
    public Material highlightRef;
    public string current;
    public GameObject bg;
    public GameObject exclaimsprite;
    public Sprite[] exclaimers;

    public int multiOrder;

    public GameObject [] NPCprefabs;
   
    public int time;
    public float timer;
    public GameObject sr;
    public GameObject item;
    public GameObject multiItem1;
    public GameObject multiItem2;
    public Sprite[] face;
    public int npcindex;
 
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
        item.SetActive(false);
        multiItem1.SetActive(false);
        multiItem2.SetActive(false);

        npcindex = Random.Range(0, NPCprefabs.Length);
        NPCprefabs[npcindex].SetActive(true);
        NPCprefabs[npcindex].transform.GetChild(1).GetComponent<MeshRenderer>().material = defaultRef;
        NPCprefabs[npcindex].transform.GetChild(0).GetComponent<MeshRenderer>().material = defaultRef;

        timer = 25.0f;
        rangeEnd = orders.Length - 1;
        exclaim.GetComponent<Animator>().Play("ExclaimNull");
        current = "none";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (current == "happy")
        {
            if(timer <= 0) {
                happy.SetActive(false);
                current = "none";
            }
            else
                timer -= 0.01f;
        }
        if(hasOrder)
        {
            //Change Face
            if(timer <= 4.0f)
            {
                sr.GetComponent<SpriteRenderer>().sprite = face[2];
                sr.GetComponent<Animator>().SetFloat("Speed", 2.5f);
                exclaimsprite.GetComponent<SpriteRenderer>().sprite = exclaimers[2];
            }
            else if(timer <= 8.0f)
            {
                sr.GetComponent<SpriteRenderer>().sprite = face[1];
                sr.GetComponent<Animator>().SetFloat("Speed", 1.5f);
                exclaimsprite.GetComponent<SpriteRenderer>().sprite = exclaimers[1];
            }
            else
            {
                sr.GetComponent<SpriteRenderer>().sprite = face[0];
                sr.GetComponent<Animator>().SetFloat("Speed", 1.0f);
                exclaimsprite.GetComponent<SpriteRenderer>().sprite = exclaimers[0];
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
        timer = 25.0f;
        multiOrder = Random.Range(0, 2);
        orderpart.Stop();
        current = "order";
        exclaim.GetComponent<Animator>().Play("ExclaimNull");
        if(multiOrder == 0) {
            int SOnum = Random.Range(0, orders.Length);
            item.GetComponent<SpriteRenderer>().sprite = orders[SOnum];
            order = item.GetComponent<SpriteRenderer>().sprite.name;
            bg.SetActive(true);
            item.SetActive(true);
        } else {
            int SOnum1 = Random.Range(0, orders.Length);
            multiItem1.GetComponent<SpriteRenderer>().sprite = orders[SOnum1];
            order1 = multiItem1.GetComponent<SpriteRenderer>().sprite.name;
            int SOnum2 = Random.Range(0, orders.Length);
            multiItem2.GetComponent<SpriteRenderer>().sprite = orders[SOnum2];
            order2 = multiItem2.GetComponent<SpriteRenderer>().sprite.name;
            bg.SetActive(true);
            multiItem1.SetActive(true);
            multiItem2.SetActive(true);
        }
        
    }

    public void spawnExclaim()
    {
        timer = 25.0f;
        orderpart.Play();
        current = "!";
        hasOrder = true;
        exclaim.GetComponent<Animator>().Play("ExclaimMain");
    } 

    public void spawnHappy()
    {
        timer = 6.0f;
        current = "happy";
        hasOrder = false;
        happy.SetActive(true);
        //happy.GetComponent<Animator>().Play("ExclaimMain");
    }


    void OnCollisionEnter(Collision other) {
         if (other.gameObject.layer == LayerMask.NameToLayer("pickup")) {
            if (order == "null" && order1 == "null" && order2 == "null") {
                Destroy(other.gameObject);
            }
            else {
                string tag = other.gameObject.tag;
                if (tag == order || tag == order1 || tag == order2) {
                    Destroy(other.gameObject);
                    if(multiOrder == 0) {
                        item.SetActive(false);
                        bg.SetActive(false);
                        burst.Play();

                        hasOrder = false;
                        current = "none";
                        spawnHappy();
                        gameHandler.GoodOrder(); //get points
                        gameHandler.numOrders--;
                    }
                    else {
                        if(tag == order1) {
                            multiItem1.SetActive(false);
                            order1 = null;
                            if(order2 == null) {
                                bg.SetActive(false);
                                burst.Play();

                                hasOrder = false;
                                current = "none";
                                spawnHappy();
                                gameHandler.GoodOrder();
                                gameHandler.numOrders--;
                            }
                        } else {
                            multiItem2.SetActive(false);
                            order2 = null;
                            if(order1 == null) {
                                bg.SetActive(false);
                                burst.Play();

                                hasOrder = false;
                                current = "none";
                                spawnHappy();
                                gameHandler.GoodOrder();
                                gameHandler.numOrders--;
                            }
                        }
                    }
                } else {
                    Destroy(other.gameObject);
                    timer -= 1f;
                }
            }
         }
     }
     
}

