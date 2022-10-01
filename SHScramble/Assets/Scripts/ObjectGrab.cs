using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ObjectGrab : MonoBehaviour
{
    private Transform objectGrabPointTransform;
    private Rigidbody rb;
    private Collider bx;
    public Image item;

    [Header("Sprites")]
    public Sprite chips;
    public Sprite soda;

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
        ChangeItem();
        item.GetComponent<Animator>().Play("ItemPU");

        this.gameObject.tag = "inHand";
    }

    public void Drop()
    {
        this.objectGrabPointTransform = null;
        rb.useGravity = true;
        this.gameObject.tag = "pickup";
        rb.constraints = RigidbodyConstraints.None;
        bx.isTrigger = false;
        item.GetComponent<Animator>().Play("ItemDrop");
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

    public void ChangeItem()
    {
        if(this.gameObject.name == "Chips")
            item.sprite = chips;
        if(this.gameObject.name == "Soda")
            item.sprite = soda;
    }
}
