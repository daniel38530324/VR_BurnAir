using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBag_CO2 : MonoBehaviour
{
    [SerializeField] Level4Manager_New level4Manager;
    [SerializeField] GameObject limeWater, limeWater_CO2;
    [SerializeField] float angle = 10;
    [SerializeField] float timeCurrentCD;
    bool isTrigger, snakeTrigger = true;
    int state;

    private void Update()
    {
        if(isTrigger)
        {
            if (timeCurrentCD <= 0)
            {
                state = 0;
            }
            else
            {
                timeCurrentCD -= Time.deltaTime;
            }

            switch (state)
            {
                case 0:
                    if (transform.localRotation.eulerAngles.z >= angle && transform.localRotation.eulerAngles.z < 180)
                    {
                        timeCurrentCD = 4;
                        state = 1;
                    }
                    break;
                case 1:
                    if (transform.localRotation.eulerAngles.z >= 360 - angle)
                    {
                        timeCurrentCD = 3;
                        state = 2;
                    }
                    break;
                case 2:
                    if (transform.localRotation.eulerAngles.z >= angle && transform.localRotation.eulerAngles.z < 180)
                    {
                        timeCurrentCD = 3;
                        state = 3;
                        limeWater_CO2.SetActive(true);
                        limeWater.SetActive(false);
                    }
                    break;
                case 3:
                    if (transform.localRotation.eulerAngles.z >= 360 - angle)
                    {
                        timeCurrentCD = 3;
                        state = 4;
                        limeWater_CO2.SetActive(true);
                        limeWater.SetActive(false);
                    }
                    break;
                case 4:
                    if (transform.localRotation.eulerAngles.z >= angle && transform.localRotation.eulerAngles.z < 180)
                    {
                        timeCurrentCD = 2;
                        state = 5;
                        limeWater_CO2.SetActive(true);
                        limeWater.SetActive(false);
                    }
                    break;
                case 5:
                    limeWater_CO2.SetActive(true);
                    limeWater.SetActive(false);
                    if (snakeTrigger)
                    {
                        snakeTrigger = false;
                        StartCoroutine(Shake());
                    }
                    break;
            }
        }
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LimeWater"))
        {
            StartCoroutine(LimeWater());
        }
    }


    IEnumerator LimeWater()
    {
        limeWater.SetActive(true);
        isTrigger = true;
        yield return new WaitForSeconds(2);
        level4Manager.UpdateLevel4State(Level4State_New.Shake);
    }

    IEnumerator Shake()
    {
        yield return new WaitForSeconds(4);
        level4Manager.UpdateLevel4State(Level4State_New.IncenseSticks);
    }
}
