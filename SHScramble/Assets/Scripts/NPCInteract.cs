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
                Debug.Log("Hi");
                GameObject objectGrab = GameObject.FindGameObjectWithTag("inHand");
                //Vector3 newScale = Vector3.Lerp(transform.localScale, Vector3.zero, 1.4f * Time.deltaTime);
                objectGrab.transform.localScale = Vector3.Lerp(objectGrab.transform.localScale, Vector3.zero, 1.4f * Time.deltaTime);
                //Destroy(objectGrab);
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
