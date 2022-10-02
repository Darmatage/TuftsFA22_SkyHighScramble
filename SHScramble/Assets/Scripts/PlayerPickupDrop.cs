using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
                        objectGrab.Grab(objectGrabPointTransform);
                    }
                }

                else if(Physics.Raycast(playerCam.position, playerCam.forward, out raycastHit, pickupDistance, pickup))
                    if (raycastHit.transform.TryGetComponent(out objectGrab))
                    {
                        grabbing = true;
                        objectGrab.Grab(objectGrabPointTransform);
                    }
            }
            else
            {
                objectGrab.Drop();
                objectGrab = null;
                grabbing = false;
            }
        }
    }
}
