using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField] Level1Manager level1Manager;
    public bool haveWind;
    public GameObject fire;
    public ParticleSystem particleSystem;
    bool isTrigger;
    bool isTrigger2;

    private void Update()
    {
        if(haveWind && !isTrigger){
            StartCoroutine(Burn());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Wind")){
            haveWind = true;
        }
    }

    IEnumerator Burn()
    {
        haveWind = false;
        isTrigger = true;
        var main = particleSystem.main;
        main.startSize = 0.1f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0.2f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0.3f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0.4f;
        yield return new WaitForSeconds(3f);
        level1Manager.UpdateLevel1State(Level1State.Cover);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cover") && !isTrigger2){
            StartCoroutine(Extinguish());
        }
    }

    IEnumerator Extinguish()
    {
        isTrigger2 = true;
        var main = particleSystem.main;
        main.startSize = 0.4f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0.3f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0.2f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0.1f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0f;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Cover")){
            StartCoroutine(Test());
        }
    }

    IEnumerator Test()
    {
        yield return new WaitForSeconds(4f);
        level1Manager.UpdateLevel1State(Level1State.Test);
    }
}
