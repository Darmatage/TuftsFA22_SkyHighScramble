using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    private GameObject triggeringNPC;
    private bool triggering;
    public PlayerPickupDrop pd;


    void Update()
    {
        if (triggering)
        {
            if (Input.GetKeyDown(KeyCode.E) && pd.grabbing)
            {
                GameObject objectGrab = GameObject.FindGameObjectWithTag("inHand");
                objectGrab.transform.localScale = Vector3.Lerp(objectGrab.transform.localScale, Vector3.zero, 1.4f * Time.deltaTime);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            triggering = true;
            triggeringNPC = other.gameObject;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC")
        {
            triggering = false;
            triggeringNPC = null;
        }
    }

}
