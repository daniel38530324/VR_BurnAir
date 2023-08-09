using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cover_PC : MonoBehaviour
{
    [SerializeField] bool isLevel4;
    [Header("Level3")]
    [SerializeField] Level3Manager_PC level3Manager;
    [SerializeField] GameObject mushroom, mushroomInWater, glassCover;
    [Header("Level4")]
    [SerializeField] Level4Manager_PC level4Manager;
    [SerializeField] GameObject soda, vinegar, plasticBag;
    [SerializeField] UnityEvent UpdateLevelState;
    
    bool isTrigger, isTrigger2, isTrigger3, isTrigger4;

    private void OnTriggerEnter(Collider other)
    {
        if(!isLevel4)
        {
            if (other.CompareTag("Mushroom") && !isTrigger)
            {
                StartCoroutine(Mushroom());
            }
            else if (other.CompareTag("H2O2") && !isTrigger2)
            {
                StartCoroutine(H2O2());
            }
            else if (other.CompareTag("GlassCover") && !isTrigger3)
            {
                other.gameObject.SetActive(false);
                StartCoroutine(GlassCover());
            }
        }
        else if(isLevel4)
        {
            if (other.CompareTag("Soda") && !isTrigger)
            {
                StartCoroutine(Soda());
            }
            else if (other.CompareTag("Vinegar") && !isTrigger2)
            {
                StartCoroutine(Vinegar());
            }
            else if (other.CompareTag("PlasticBag") && !isTrigger3)
            {
                other.gameObject.SetActive(false);
                StartCoroutine(PlasticBag());
            }
            else if (other.CompareTag("GlassCover") && !isTrigger4)
            {
                other.gameObject.SetActive(false);
                StartCoroutine(GlassCover2());
            }
        }
       
    }

    IEnumerator Mushroom()
    {
        isTrigger = true;
        mushroom.SetActive(true);
        yield return new WaitForSeconds(2);
        level3Manager.UpdateLevel3State(Level3State_PC.H2O2);
    }

    IEnumerator H2O2()
    {
        isTrigger2 = true;
        mushroomInWater.SetActive(true);
        yield return new WaitForSeconds(2);
        level3Manager.UpdateLevel3State(Level3State_PC.GlassCover);
    }

    IEnumerator GlassCover()
    {
        isTrigger3 = true;
        glassCover.SetActive(true);
        yield return new WaitForSeconds(2);
        level3Manager.UpdateLevel3State(Level3State_PC.IncenseSticks);
    }

    IEnumerator Soda()
    {
        isTrigger = true;
        soda.SetActive(true);
        yield return new WaitForSeconds(2);
        level4Manager.UpdateLevel4State(Level4State_PC.Vinegar);
    }

    IEnumerator Vinegar()
    {
        isTrigger2 = true;
        vinegar.SetActive(true);
        yield return new WaitForSeconds(2);
        level4Manager.UpdateLevel4State(Level4State_PC.PlasticBag);
    }

    IEnumerator PlasticBag()
    {
        isTrigger3 = true;
        plasticBag.SetActive(true);
        yield return new WaitForSeconds(2);
        level4Manager.UpdateLevel4State(Level4State_PC.GlassCover);
    }

    IEnumerator GlassCover2()
    {
        isTrigger4 = true;
        glassCover.SetActive(true);
        yield return new WaitForSeconds(2);
        level4Manager.UpdateLevel4State(Level4State_PC.LimeWater);
    }


}
