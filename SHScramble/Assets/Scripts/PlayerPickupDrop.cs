using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPickupDrop : MonoBehaviour
{

    [SerializeField] private Transform playerCam;
    [SerializeField] private LayerMask pickup;
    [SerializeField] private LayerMask box;
    [SerializeField] private Transform objectGrabPointTransform;
    public float pickupDistance = 2f;

    private ObjectGrab objectGrab;
    private BoxPickup bp;
    public bool grabbing;
    public Image button;
    private string curTag;

    [SerializeField] audioItemPickDrop soundGenerator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!grabbing)
            {
                if(Physics.Raycast(playerCam.position, playerCam.forward, out RaycastHit raycastHit, pickupDistance, box))
                {
                    if (raycastHit.transform.TryGetComponent(out bp))
                    {
                        objectGrab = bp.Summon();
                        grabbing = true;
                        curTag = objectGrab.Grab(objectGrabPointTransform);
                        soundGenerator.audioSource.clip = soundGenerator.itemSound[0];
                        soundGenerator.audioSource.Play();
                    }
                }

                else if(Physics.Raycast(playerCam.position, playerCam.forward, out raycastHit, pickupDistance, pickup))
                    if (raycastHit.transform.TryGetComponent(out objectGrab))
                    {
                        grabbing = true;
                        curTag = objectGrab.Grab(objectGrabPointTransform);
                        soundGenerator.audioSource.clip = soundGenerator.itemSound[0];
                        soundGenerator.audioSource.Play();
                    }
            }
            else
            {
                objectGrab.Drop(curTag);
                soundGenerator.audioSource.clip = soundGenerator.itemSound[1];
                soundGenerator.audioSource.Play();
                objectGrab = null;
                grabbing = false;
            }
        }
    }
}
