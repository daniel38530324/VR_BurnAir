using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReturnPosition : MonoBehaviour
{
    [SerializeField] UnityEvent ReturnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Floor"))
        {
            ReturnPoint.Invoke();
        }
    }
}
