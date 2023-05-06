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

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MnO2"))
        {
            StartCoroutine(MnO2());
        }
        else if (other.CompareTag("Water"))
        {
            StartCoroutine(Water());
        }
        else if (other.CompareTag("H2O2"))
        {
            StartCoroutine(H2O2());
        }
        else if (other.CompareTag("CaCO3"))
        {
            StartCoroutine(CaCO3());
        }
        else if (other.CompareTag("HCl"))
        {
            StartCoroutine(HCl());
        }
    }

    IEnumerator MnO2()
    {
        yield return new WaitForSeconds(2);
        mnO2.SetActive(true);
        level3Manager.UpdateLevel3State(Level3State.Water);
    }

    IEnumerator Water()
    {
        yield return new WaitForSeconds(2);
        UpdateLevelState.Invoke();
        //level3Manager.UpdateLevel3State(Level3State.H2O2);
    }

    IEnumerator H2O2()
    {
        yield return new WaitForSeconds(2);
        level3Manager.UpdateLevel3State(Level3State.Tube);
    }

    IEnumerator CaCO3()
    {
        yield return new WaitForSeconds(2);
        caco3.SetActive(true);
        level4Manager.UpdateLevel4State(Level4State.Water);
    }

    IEnumerator HCl()
    {
        yield return new WaitForSeconds(2);
        level4Manager.UpdateLevel4State(Level4State.Tube);
    }
}
