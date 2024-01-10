using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBag_CO2_PC : MonoBehaviour
{
    [SerializeField] Level4Manager_PC level4Manager;
    [SerializeField] GameObject limeWater;
    bool isTrigger;

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
}
