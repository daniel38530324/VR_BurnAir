using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clip : MonoBehaviour
{
    [SerializeField] Transform followPoint;
    GameObject steelWool;
    bool trigger;
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
                steelWool.transform.position = followPoint.transform.position;
            }
        }
        else
        {
            if(steelWool != null){
                steelWool.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("SteelWool"))
        {
            steelWool = other.gameObject;
            steelWool.GetComponent<Rigidbody>().isKinematic = true;
            trigger = true;
        }
    }

    public void clip(bool success)
    {
        if (success)
        {
            collider.enabled = true;
        }
        else
        {
            collider.enabled = false;
            trigger = false;
        }
    }
}
