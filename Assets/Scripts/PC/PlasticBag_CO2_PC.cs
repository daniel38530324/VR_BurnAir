using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBag_CO2_PC : MonoBehaviour
{
    [SerializeField] Level4Manager_PC level4Manager;
    [SerializeField] GameObject limeWater, limeWater_CO2;
    bool isTrigger, isTrigger2;

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
        level4Manager.UpdateLevel4State(Level4State_PC.Shake);
    }

    public void Shake()
    {
        if(!isTrigger2 && level4Manager.level4State == Level4State_PC.Shake){
            limeWater_CO2.SetActive(true);
            limeWater.SetActive(false);
            StartCoroutine(ShakeThis());
        }
    }

    IEnumerator ShakeThis()
    {
        yield return new WaitForSeconds(4);
        level4Manager.UpdateLevel4State(Level4State_PC.IncenseSticks);
    }
}
