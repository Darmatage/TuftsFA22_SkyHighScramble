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
    public bool looking = false;
    public Transform playerCam;
    public GameObject other;

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
        //Debug.DrawRay(playerCam.position, playerCam.forward, Color.white, 0.0f, true);
        //Debug.Log(looking);
        if(Physics.Raycast(playerCam.position, playerCam.forward, out RaycastHit raycastHit, 2f) && raycastHit.transform.tag == "NPC")
        { 
            other = raycastHit.transform.gameObject;
            os = other.GetComponent<OrderSpawner>();
            if ((os.current == "!") || (os.current == "order"))
            {
                button.GetComponent<EButton>().Near("Talk");
                highlightRef = other.GetComponent<OrderSpawner>().highlightRef;
                int npcindex = other.GetComponent<OrderSpawner>().npcindex;
                other.transform.GetChild(0).GetChild(npcindex).GetChild(0).GetComponent<MeshRenderer>().material = highlightRef;
                other.transform.GetChild(0).GetChild(npcindex).GetChild(1).GetComponent<MeshRenderer>().material = highlightRef;
            }
            triggering = true;
            triggeringNPC = other.gameObject;
        } else if (other != null) {
            button.GetComponent<EButton>().Away();
            triggering = false;
            triggeringNPC = null;
            defaultRef = other.GetComponent<OrderSpawner>().defaultRef;
            int npcindex = other.GetComponent<OrderSpawner>().npcindex;
            other.transform.GetChild(0).GetChild(npcindex).GetChild(0).GetComponent<MeshRenderer>().material = defaultRef;
            other.transform.GetChild(0).GetChild(npcindex).GetChild(1).GetComponent<MeshRenderer>().material = defaultRef;
            os = null;
        }
                
}


}

