using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle_Control : MonoBehaviour
{
    [SerializeField] Level1Manager level1Manager;
    [SerializeField] GameObject warn_UI;

    [Header("Fan")]
    [SerializeField] GameObject Fan;

    [Header("Bucket")]
    [SerializeField] WaterBucket_New waterBucket;
    [SerializeField] GameObject water, waterEffect;

    [Header("Flour")]
    [SerializeField] WaterBucket_New flourBag;
    [SerializeField] GameObject flour, flourEffect;


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wind"))
        {
            Fan.SetActive(false);
            StartCoroutine(RetuenState(Level1State.Fan));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cover"))
        {
            StartCoroutine(RetuenState(Level1State.Cover));
        }

        if (other.CompareTag("Water"))
        {
            waterBucket.gameObject.SetActive(false);
            waterBucket.enabled = false;
            water.SetActive(true);
            waterEffect.SetActive(false);
            StartCoroutine(RetuenState(Level1State.Bucket));
        }

        if (other.CompareTag("Flour"))
        {
            flourBag.gameObject.SetActive(false);
            flourBag.enabled = false;
            flour.SetActive(true);
            flourEffect.SetActive(false);
            StartCoroutine(RetuenState(Level1State.Flour));
        }
    }

    IEnumerator RetuenState(Level1State returnState)
    {
        warn_UI.SetActive(true);
        yield return new WaitForSeconds(5);
        level1Manager.UpdateLevel1State(returnState);
        warn_UI.SetActive(false);
    }
}
