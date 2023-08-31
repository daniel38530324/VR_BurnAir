using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle_Control : MonoBehaviour
{
    [SerializeField] Level1Manager level1Manager;
    [SerializeField] GameObject warn_UI;

    [Header("Fan")]
    [SerializeField] GameObject Fan;
    
    [Header("Cover")]
    [SerializeField] GameObject cover;

    [Header("Bucket")]
    [SerializeField] WaterBucket_New waterBucket;
    [SerializeField] GameObject water, waterEffect;

    [Header("Flour")]
    [SerializeField] WaterBucket_New flourBag;
    [SerializeField] GameObject flour, flourEffect;
    public bool isTrigger;

    [Header("ForLevel1")]
    public WaterBucket_New waterBucket_New;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wind"))
        {
            Fan.SetActive(false);
            StartCoroutine(ReturnState(Level1State.Fan));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cover") && !isTrigger)
        {
            cover.SetActive(false);
            StartCoroutine(ReturnState(Level1State.Cover));
        }

        if (other.CompareTag("Water") && !isTrigger)
        {
            waterBucket.gameObject.SetActive(false);
            waterBucket.enabled = false;
            water.SetActive(true);
            waterEffect.SetActive(false);
            StartCoroutine(ReturnState(Level1State.Bucket));
        }

        if (other.CompareTag("Flour") && !isTrigger)
        {
            flourBag.gameObject.SetActive(false);
            flourBag.enabled = false;
            flour.SetActive(true);
            flourEffect.SetActive(false);
            StartCoroutine(ReturnState(Level1State.Flour));
        }
    }

    IEnumerator ReturnState(Level1State returnState)
    {
        if(!waterBucket_New.isReturn){
            isTrigger = true;
            warn_UI.SetActive(true);
            AudioManager.Instance.PlaySound("Control");
            yield return new WaitForSeconds(3);
            level1Manager.ReturnLevelState(returnState);
            warn_UI.SetActive(false);
            isTrigger = false;

            if(returnState == Level1State.Bucket){
                waterBucket.enabled = true;
            }else if(returnState == Level1State.Flour){
                flourBag.enabled = true;
            }
        }
    }
}
