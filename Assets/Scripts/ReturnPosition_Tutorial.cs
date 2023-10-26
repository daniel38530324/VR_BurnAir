using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReturnPosition_Tutorial : MonoBehaviour
{
    [SerializeField] Transform returnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Floor"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.position = returnPoint.position;
            transform.rotation = returnPoint.rotation;
            GetComponent<Rigidbody>().isKinematic = false;
        }
    }
}
