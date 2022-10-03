using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCInteract : MonoBehaviour
{
    private GameObject triggeringNPC;
    private bool triggering;
    public PlayerPickupDrop pd;
    public Image button;
    public OrderSpawner os;

    void Update()
    {
        if (triggering && Input.GetKeyDown(KeyCode.E))
        {
            if (os.current == "!")
            {
                os.spawnOrder();
            }
            else if (os.current == "order")
            {

            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            if ((os.current == "!") || (os.current == "order"))
                button.GetComponent<EButton>().Near("Talk");
            triggering = true;
            triggeringNPC = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC")
        {
            button.GetComponent<EButton>().Away();
            triggering = false;
            triggeringNPC = null;
        }
    }

}
