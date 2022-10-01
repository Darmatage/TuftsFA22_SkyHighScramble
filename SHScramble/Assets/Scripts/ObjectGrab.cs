using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrab : MonoBehaviour
{
    private Transform objectGrabPointTransform;
    private Rigidbody rb;
    private Collider bx;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bx = GetComponent<Collider>();
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        bx.isTrigger = true;
        this.gameObject.tag = "inHand";
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        rb.useGravity = true;
        this.gameObject.tag = "pickup";
    }
    // Update is called once per frame
    void Update()
    {
        if (objectGrabPointTransform != null)
        {
            //float lerpSpeed = 5f;
            //Vector3 newPosition = Vector3.Lerp(transform.position, objectGrabPointTransform.position, Time.deltaTime * lerpSpeed);
            rb.velocity = (Vector3.zero);
            rb.MovePosition(objectGrabPointTransform.position);
        }
    }
}
