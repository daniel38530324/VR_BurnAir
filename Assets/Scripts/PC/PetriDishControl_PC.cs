using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetriDishControl_PC : MonoBehaviour
{
    [SerializeField] Level5Manager_PC level5Manager;
    [SerializeField] GameObject warn_UI;

    public bool isTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("H2O2") && !isTrigger)
        {
            StartCoroutine(ReturnState(Level5State_PC.Water));
        }

        if (other.CompareTag("Vinegar") && !isTrigger)
        {
            StartCoroutine(ReturnState(Level5State_PC.Vinegar));
        }
    }

    IEnumerator ReturnState(Level5State_PC returnState)
    {
        isTrigger = true;
        warn_UI.SetActive(true);
        yield return new WaitForSeconds(3);
        warn_UI.SetActive(false);
        isTrigger = false;
    }
}
