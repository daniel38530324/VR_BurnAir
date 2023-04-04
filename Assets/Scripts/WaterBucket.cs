using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucket : MonoBehaviour
{
    public GameObject waterEffect;
    public GameObject water;
    bool trigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Water") && !trigger){
            trigger = true;
            waterEffect.SetActive(true);
            water.SetActive(false);
        }
    }
}
