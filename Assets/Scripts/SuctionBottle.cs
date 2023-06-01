using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SuctionBottle : MonoBehaviour
{
    [SerializeField] Level3Manager level3Manager;
    [SerializeField] Level4Manager level4Manager;
    [SerializeField] GameObject mnO2, caco3;
    [SerializeField] UnityEvent UpdateLevelState;
    public bool isTrigger, isTrigger2, isTrigger3;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MnO2") && !isTrigger)
        {
            StartCoroutine(MnO2());
        }
        else if (other.CompareTag("Water") && !isTrigger2)
        {
            StartCoroutine(Water());
        }
        else if (other.CompareTag("H2O2") && !isTrigger3)
        {
            StartCoroutine(H2O2());
        }
        else if (other.CompareTag("CaCO3") && !isTrigger)
        {
            StartCoroutine(CaCO3());
        }
        else if (other.CompareTag("HCl") && !isTrigger3)
        {
            StartCoroutine(HCl());
        }
    }

    IEnumerator MnO2()
    {
        isTrigger = true;
        yield return new WaitForSeconds(2);
        mnO2.SetActive(true);
        level3Manager.UpdateLevel3State(Level3State.Water);
    }

    IEnumerator Water()
    {
        isTrigger2 = true;
        yield return new WaitForSeconds(2);
        UpdateLevelState.Invoke();
        //level3Manager.UpdateLevel3State(Level3State.H2O2);
    }

    IEnumerator H2O2()
    {
        isTrigger3 = true;
        yield return new WaitForSeconds(2);
        level3Manager.UpdateLevel3State(Level3State.GlassCover);
    }

    IEnumerator CaCO3()
    {
        isTrigger = true;
        yield return new WaitForSeconds(2);
        caco3.SetActive(true);
        level4Manager.UpdateLevel4State(Level4State.Water);
    }

    IEnumerator HCl()
    {
        isTrigger3 = true;
        yield return new WaitForSeconds(2);
        level4Manager.UpdateLevel4State(Level4State.GlassCover);
    }
}
