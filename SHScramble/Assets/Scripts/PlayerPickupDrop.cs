using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupDrop : MonoBehaviour
{

    [SerializeField] private Transform playerCam;
    [SerializeField] private LayerMask pickup;
    [SerializeField] private Transform objectGrabPointTransform;
    public float pickupDistance = 2f;

    private ObjectGrab objectGrab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrab == null)
            {
                if(Physics.Raycast(playerCam.position, playerCam.forward, out RaycastHit raycastHit, pickupDistance, pickup))
                if (raycastHit.transform.TryGetComponent(out objectGrab))
                {
                    objectGrab.Grab(objectGrabPointTransform);
                }
            }
            else
            {
                objectGrab.Drop();
                objectGrab = null;
            }
        }
    }
}
