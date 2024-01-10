using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBag_CO2 : MonoBehaviour
{
    [SerializeField] Level4Manager_New level4Manager;
    [SerializeField] GameObject limeWater;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("LimeWater"))
        {
            StartCoroutine(LimeWater());
        }
    }

    IEnumerator LimeWater()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        limeWater.SetActive(true);
        yield return new WaitForSeconds(2);
        level4Manager.UpdateLevel4State(Level4State_New.Shake);
    }
}
