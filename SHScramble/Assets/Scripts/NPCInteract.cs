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
    private OrderSpawner os;

    private Material highlightRef;
    private Material defaultRef;

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
            os = other.GetComponent<OrderSpawner>();
            if ((os.current == "!") || (os.current == "order"))
            {
                button.GetComponent<EButton>().Near("Talk");
                highlightRef = other.GetComponent<OrderSpawner>().highlightRef;
                other.transform.GetChild(1).GetComponent<MeshRenderer>().material = highlightRef;
            }
            triggering = true;
            triggeringNPC = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC")
        {
            os = null;
            button.GetComponent<EButton>().Away();
            triggering = false;
            triggeringNPC = null;
            defaultRef = other.GetComponent<OrderSpawner>().defaultRef;
            other.transform.GetChild(1).GetComponent<MeshRenderer>().material = defaultRef;
        }
    }

}
