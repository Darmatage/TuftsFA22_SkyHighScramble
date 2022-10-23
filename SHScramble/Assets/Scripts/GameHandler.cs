using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class GameHandler : MonoBehaviour
{
    public OrderSpawner[] customer;

    private float spawnRangeStart = 4.0f;
    private float spawnRangeEnd= 10.0f;
    private float timeToSpawn = 10.0f;
    private float spawnTimer = 0f;
    [Header("Game Variables")]
    public int[] maxOrders;
    private float gameTime = 0;
    public float[] totalTime;
    public int[] numNPC;
    public int[] ordersNeeded;
    private int doneOrders = 0;
    public int[] numButtons;
    public GameObject canva;
    public VideoPlayer videop;

    public int numOrders;
    public float happiness = 100f;

    [Header("Object Refs")]
    public Sprite[] mark;
    public GameObject[] buttons;
    public GameObject[] stars;
    public GameObject sun;
    public Image exclaimmark;
    public Text ordertext;
    public Text happytext;
    public Text[] startext;
    public Slider tslider;
    public Slider happyMeter;
    public Image healthFill;
    private float lerpSpeed;
    public Material[] skys;
    [Header("NPC Seats")]
    public GameObject[] spots;
    public GameObject defaultNPC;
    public GameObject parenty;
    public bool tutorial;
    public GameObject wall;


    void Start()
    {
        StartCoroutine(ReadySet());
        //Number Buttons
        for (int r = 0; r < numButtons[LevelHandler.playlev]; r++)
        {
            buttons[r].SetActive(true);
        }


        startext[2].text = "" + ordersNeeded[LevelHandler.playlev];
        startext[1].text = "" + (ordersNeeded[LevelHandler.playlev]/2);
        startext[0].text = "" + (ordersNeeded[LevelHandler.playlev]/4);
        //Change Sky
        int skymatnum = Random.Range(0, 2);
        RenderSettings.skybox = skys[skymatnum];
        
        if (skymatnum >= 1)
            sun.GetComponent<Light>().color = new Color(95f/255f, 122f/255f, 165f/255f);
        else
            sun.GetComponent<Light>().color = Color.white;

        int npcsize = 24;
        if(tutorial)
        {
            npcsize = 12;
            wall.SetActive(true);
        }


        //cap the number of npcs to seat #s
        if (numNPC[LevelHandler.playlev] > spots.Length)
            numNPC[LevelHandler.playlev] = spots.Length;

        customer = new OrderSpawner[numNPC[LevelHandler.playlev]];
        int tempNPC = numNPC[LevelHandler.playlev];
        int fillNum = 0;
        while (tempNPC > 0)
        {
            int spotNum = Random.Range(0, spots.Length);
            if ((spots[spotNum].tag != "hasPlayer") && (spotNum < npcsize))
            {
                GameObject nick = Instantiate(defaultNPC, spots[spotNum].transform.position, Quaternion.identity);
                nick.layer = 6;
                nick.transform.parent = parenty.transform;
                spots[spotNum].tag = "hasPlayer";
                customer[fillNum] = (nick.GetComponent<OrderSpawner>());
                tempNPC--;
                fillNum++;
            }
        }
        numNPC[LevelHandler.playlev] = customer.Length;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lerpSpeed = 3f * Time.deltaTime;

        gameTime += 0.02f;
        tslider.value = ((gameTime/totalTime[LevelHandler.playlev])/2 + 0.5f);
        happyMeter.value = (happiness/100);

        int j;
        if (doneOrders >= ordersNeeded[LevelHandler.playlev]) {j = 3;}
        else if (doneOrders >= ordersNeeded[LevelHandler.playlev]/2) {j = 2;}
        else if (doneOrders >= ordersNeeded[LevelHandler.playlev]/4) {j = 1;}
        else {j = 0;}

        for (int i = 0; i < j; i++)
        {
            stars[i].SetActive(true);
            startext[i].text = "";
            //stars[i].GetComponent<Animator>().Play("Star");
        }

        ordertext.text = "0" + numOrders;
        happytext.text = "0" + doneOrders;
        if (numOrders >= 3)
        {
            exclaimmark.sprite = mark[2];
            exclaimmark.GetComponent<Animator>().SetFloat("speed", 2f);
            exclaimmark.GetComponent<Animator>().Play("Exclaim3");
        }
        else if (numOrders >= 2)
        {
            exclaimmark.sprite = mark[1];
            exclaimmark.GetComponent<Animator>().SetFloat("speed", 1.5f);
            exclaimmark.GetComponent<Animator>().Play("Exclaim3");
        }
        else if (numOrders >= 1)
        {
            exclaimmark.sprite = mark[0];
            exclaimmark.GetComponent<Animator>().SetFloat("speed", 1f);
            exclaimmark.GetComponent<Animator>().Play("Exclaim3");
        }
        else
            exclaimmark.GetComponent<Animator>().Play("Nothing");

        spawnTimer += 0.02f;
        if (spawnTimer >= timeToSpawn){
            if (numOrders < maxOrders[LevelHandler.playlev])
            {
                int npc = Random.Range(0, numNPC[LevelHandler.playlev]);
                while ((customer[npc].hasOrder) || customer[npc].current == "happy") 
                    npc = Random.Range(0, numNPC[LevelHandler.playlev]);
                customer[npc].spawnExclaim();
                numOrders++;
            }
            spawnTimer = 0f;
            timeToSpawn = Random.Range(spawnRangeStart, spawnRangeEnd);
        }


        ColorChanger();

        if(gameTime >= totalTime[LevelHandler.playlev]) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameTime = 0;

            this.GetComponent<PauseMenu>().Win(doneOrders, ordersNeeded[LevelHandler.playlev]); 
        }

        // if (happiness <= 0)
        // {
        //     Cursor.lockState = CursorLockMode.None;
        //     Cursor.visible = true;
        //     gameTime = 0;
        //     happiness = 50;
        //     //SceneManager.LoadScene("GameOver");
        //     this.GetComponent<PauseMenu>().Lose(); 
        // }
        

    }
    
    public void GoodOrder()
    {
        happyMeter.value = Mathf.Lerp(happyMeter.value, happyMeter.value + 0.2f, lerpSpeed);
        happiness += 20f;
        doneOrders++;
    }

    public void FailedOrder()
    {
        happiness -= 10f;
    }

    void ColorChanger()
    {
        Color barColor = Color.Lerp(Color.red, Color.green, (happiness/100f));
        healthFill.color = barColor;
    }

    IEnumerator ReadySet()
    {
        //yield return new WaitForSeconds(1.0f);
        videop.Play();
        yield return new WaitForSeconds(3f);
        canva.SetActive(false);
    }
}