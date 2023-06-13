using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clip : MonoBehaviour
{
    [SerializeField] Transform followPoint;
    GameObject steelWool;
    bool trigger;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(trigger)
        {
            steelWool.transform.position = followPoint.transform.position;
        }
        else
        {
            if(steelWool)
            {
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
            GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            GetComponent<BoxCollider>().enabled = false;
            trigger = false;
        }
    }
}
