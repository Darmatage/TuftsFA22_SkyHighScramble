using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPickup : MonoBehaviour
{
    public GameObject Prefab;
    private ObjectGrab objectGrab;
    // Start is called before the first frame update
    public ObjectGrab Summon()
    {
        GameObject item = (GameObject)Instantiate(Prefab);
        objectGrab = item.transform.GetComponent<ObjectGrab>();
        return objectGrab;
    }
}
