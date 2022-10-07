using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public OrderSpawner[] customer;

    private float spawnRangeStart = 4.0f;
    private float spawnRangeEnd= 10.0f;
    private float timeToSpawn = 10.0f;
    private float spawnTimer = 0f;
    [Header("Game Variables")]
    public int maxOrders = 3;
    private float gameTime = 0;
    public float happiness = 100f;
    public int numNPC;

    private int numOrders;

    [Header("Object Refs")]
    public Slider tslider;
    public Slider happyMeter;
    public Image healthFill;
    private float lerpSpeed;
    [Header("NPC Seats")]
    public GameObject[] spots;
    public GameObject defaultNPC;


    void Start()
    {
        //cap the number of npcs to seat #s
        if (numNPC > spots.Length)
            numNPC = spots.Length;

        customer = new OrderSpawner[numNPC];
        int tempNPC = numNPC;
        int fillNum = 0;
        while (tempNPC > 0)
        {
            int spotNum = Random.Range(0, spots.Length);
            if (spots[spotNum].tag != "hasPlayer")
            {
                GameObject nick = Instantiate(defaultNPC, spots[spotNum].transform.position, Quaternion.identity);
                spots[spotNum].tag = "hasPlayer";
                customer[fillNum] = (nick.GetComponent<OrderSpawner>());
                tempNPC--;
                fillNum++;
            }
        }
        numNPC = customer.Length;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lerpSpeed = 3f * Time.deltaTime;

        gameTime += 0.02f;
        tslider.value = (gameTime/100);
        happyMeter.value = (happiness/100);

        if (numOrders != 0)
        {
            happiness -= 0.02f * numOrders;
        }

        spawnTimer += 0.02f;
        if (spawnTimer >= timeToSpawn){
            if (numOrders <= maxOrders)
            {
                int npc = Random.Range(0, numNPC);
                while (customer[npc].hasOrder)
                    npc = Random.Range(0, numNPC);
                customer[npc].spawnExclaim();
                numOrders++;
            }
            spawnTimer = 0f;
            timeToSpawn = Random.Range(spawnRangeStart, spawnRangeEnd);
        }


        ColorChanger();

        if ((gameTime >= 100) || (happiness <= 0))
        {

        }

    }
    
    public void GoodOrder()
    {
        happyMeter.value = Mathf.Lerp(happyMeter.value, happyMeter.value + 0.2f, lerpSpeed);
        happiness += 20f;
    }

    public void FailedOrder()
    {
        happiness -= 20f;
    }

    void ColorChanger()
    {
        Color barColor = Color.Lerp(Color.red, Color.green, (happiness/100f));
        healthFill.color = barColor;
    }
}