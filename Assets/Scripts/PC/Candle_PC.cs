using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle_PC : MonoBehaviour
{
    [SerializeField] Level1Manager_PC level1Manager;
    public bool haveWind;
    public GameObject fire;
    public ParticleSystem particleSystem;
    public GameObject particleSystem2;
    bool isTrigger;
    public bool isTrigger2;

    int state = 0;

    private void Update()
    {
        if(haveWind && !isTrigger && state == 0){
            StartCoroutine(Burn());
            StartCoroutine(NextState(Level1State_PC.Cover));
            state = 1;
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

    IEnumerator Burn2()
    {
        haveWind = false;
        isTrigger = true;
        var main = particleSystem.main;
        main.startSize = 1f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 2f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 4f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 2f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 1f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0.1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cover") && !isTrigger2 && state == 1){
            state = 2;
            StartCoroutine(Extinguish());
            StartCoroutine(NextState(Level1State_PC.Bucket));
        }

        if(other.CompareTag("Water") && state == 2)
        {
            state = 3;
            StartCoroutine(Extinguish());
            StartCoroutine(NextState(Level1State_PC.Flour));
        }

        if (other.CompareTag("Flour") && state == 3)
        {
            state = 4;
            StartCoroutine(Burn2());
            StartCoroutine(NextState(Level1State_PC.Test));
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
        level1Manager.UpdateLevel1State(Level1State_PC.Bucket);
        isTrigger2 = false;
    }

    IEnumerator NextState(Level1State_PC nextState)
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