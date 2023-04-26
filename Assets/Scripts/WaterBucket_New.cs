using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucket_New : MonoBehaviour
{
    [SerializeField] GameObject waterEffect, water;

    bool trigger = true;

    private void OnEnable()
    {
        trigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!trigger)
        {
            //Debug.Log(transform.eulerAngles.x);
            if (Quaternion.Angle(transform.rotation, Quaternion.identity) >= 120)
            {
                waterEffect.SetActive(true);
                water.SetActive(false);
                trigger = true;
            }
        }
    }
}
