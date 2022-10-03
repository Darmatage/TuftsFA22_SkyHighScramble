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
