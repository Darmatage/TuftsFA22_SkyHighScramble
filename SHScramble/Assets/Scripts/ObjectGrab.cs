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
    private string curTag;

    [Header("Sprites")]
    public Sprite[] items;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        bx = GetComponent<Collider>();
    }

    public string Grab(Transform objectGrabPointTransform)
    {
        this.objectGrabPointTransform = objectGrabPointTransform;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezePosition;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        bx.isTrigger = true;
        ChangeItem();
        item.GetComponent<Animator>().Play("ItemPU");

        curTag = this.gameObject.tag;
        this.gameObject.tag = "inHand";
        return curTag;
    }

    public void Drop(string curTag)
    {
        this.objectGrabPointTransform = null;
        rb.useGravity = true;
        this.gameObject.tag = curTag;
        rb.constraints = RigidbodyConstraints.None;
        bx.isTrigger = false;
        item.GetComponent<Animator>().Play("ItemDrop");
    }
    // Update is called once per frame
    void Update()
    {
        if (objectGrabPointTransform != null)
        {
            rb.velocity = (Vector3.zero);
            rb.MovePosition(objectGrabPointTransform.position);
        }
    }

    public void ChangeItem()
    {
        if(this.gameObject.tag == "Chips")
            item.sprite = items[0];
        if(this.gameObject.tag == "Soda")
            item.sprite = items[1];
        if(this.gameObject.tag == "BarfBag")
            item.sprite = items[2];
        if(this.gameObject.tag == "Headphones")
            item.sprite = items[3];
        if(this.gameObject.tag == "Water")
            item.sprite = items[4];
        if(this.gameObject.tag == "Wine")
            item.sprite = items[5];
        if(this.gameObject.tag == "Napkin")
            item.sprite = items[6];
    }
}
