using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField] Level1Manager level1Manager;
    public bool haveWind;
    public GameObject fire;
    public ParticleSystem particleSystem;
    public GameObject particleSystem2;
    bool isTrigger;
    bool isTrigger2;

    private void Update()
    {
        if(haveWind && !isTrigger){
            StartCoroutine(Burn());
            StartCoroutine(NextState(Level1State.Cover));
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cover") && !isTrigger2){
            StartCoroutine(Extinguish());
        }

        if(other.CompareTag("Water"))
        {
            StartCoroutine(Extinguish());
            StartCoroutine(NextState(Level1State.Flour));
        }

        if (other.CompareTag("Flour"))
        {
            StartCoroutine(Burn());
            StartCoroutine(NextState(Level1State.Test));
        }
    }

    IEnumerator Extinguish()
    {
        isTrigger2 = true;
        var main = particleSystem.main;
        main.startSize = 0.1f;
        particleSystem2.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0.07f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0.04f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0.01f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0f;
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Cover")){
            StartCoroutine(Bucket());
        }
    }

    IEnumerator Bucket()
    {
        yield return new WaitForSeconds(4f);
        level1Manager.UpdateLevel1State(Level1State.Bucket);
        isTrigger2 = false;
    }

    IEnumerator NextState(Level1State nextState)
    {
        yield return new WaitForSeconds(4f);
        level1Manager.UpdateLevel1State(nextState);
    }

    public void ReturnFire()
    {
        var main = particleSystem.main;
        main.startSize = 0.1f;
        particleSystem2.SetActive(true);
    }
}
