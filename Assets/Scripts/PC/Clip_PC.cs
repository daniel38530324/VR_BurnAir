using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clip_PC : MonoBehaviour
{
    GameObject steelWool;
    public bool trigger;
    BoxCollider collider;

    void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if(trigger)
        {
            if(steelWool != null){
                steelWool.transform.position = transform.position;
            }
        }
        else
        {
            if(steelWool != null){
                steelWool.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }
}
