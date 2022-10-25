using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TriggerLevel : MonoBehaviour
{
    public Material[] mats;
    private Animator camanim;
    public Camera cam;
    public GameObject ticket;
    public GameObject lev;
    public int stars;
    private Animator taganim;
    private Animator levanim;
    public bool isLocked;

    public int level;

    // Start is called before the first frame update
    void Start()
    {
        if(isLocked)
        {
            this.transform.GetChild(4).gameObject.SetActive(true);
        }
        ticket.SetActive(false);
        lev.SetActive(false);
        camanim = cam.GetComponent<Animator>();
        taganim = ticket.GetComponent<Animator>();
        levanim = lev.GetComponent<Animator>();
    }

    void Update()
    {
        stars = LevelHandler.curstars[level];
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if(!isLocked)
        {
            if(other.tag == "Player")
            {
                LevelHandler.playlev = level;
                ticket.SetActive(true);
                lev.SetActive(true);
                this.transform.GetChild(1).GetComponent<MeshRenderer>().material = mats[1];

                lev.transform.GetChild(1).GetComponent<Text>().text = this.name;
                for(int i = 0; i < 3; i++)
                    ticket.transform.GetChild(1).GetChild(i).gameObject.SetActive(false);
                for(int i = 0; i < stars; i++)
                    ticket.transform.GetChild(1).GetChild(i).gameObject.SetActive(true);


                camanim.Play("CamGlobe");
                taganim.Play("TagOn");
                levanim.Play("LevOn");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(!isLocked)
            if(other.tag == "Player")
            {
                this.transform.GetChild(1).GetComponent<MeshRenderer>().material = mats[0];
                camanim.Play("CamGlobeBack");
                taganim.Play("TagOff");
                levanim.Play("LevOff");
            }
    }
}
