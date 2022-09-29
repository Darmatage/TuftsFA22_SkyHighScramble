using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectGrab : MonoBehaviour
{
    private Transform objectGrabPointTransform;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        rb.useGravity = false;
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        rb.useGravity = true;
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
