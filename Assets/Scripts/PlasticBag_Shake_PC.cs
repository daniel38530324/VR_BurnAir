using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlasticBag_Shake_PC : MonoBehaviour
{
    [SerializeField] Level4Manager_PC level4Manager;
    [SerializeField] GameObject limeWater_CO2;
    bool isTrigger;

    public void Shake()
    {
        if(!isTrigger && level4Manager.level4State == Level4State_PC.Shake){
            limeWater_CO2.SetActive(true);
            StartCoroutine(ShakeThis());
        }
    }

    IEnumerator ShakeThis()
    {
        yield return new WaitForSeconds(4);
        level4Manager.UpdateLevel4State(Level4State_PC.IncenseSticks);
    }
}
