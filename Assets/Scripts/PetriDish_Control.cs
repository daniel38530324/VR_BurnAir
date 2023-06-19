using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetriDish_Control : MonoBehaviour
{
    [SerializeField] Level5Manager level5Manager;
    [SerializeField] GameObject warn_UI;

    public bool isTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("H2O2") && !isTrigger)
        {
            StartCoroutine(ReturnState(Level5State.Water));
        }

        if (other.CompareTag("Vinegar") && !isTrigger)
        {
            StartCoroutine(ReturnState(Level5State.Vinegar));
        }


    }

    IEnumerator ReturnState(Level5State returnState)
    {
        isTrigger = true;
        warn_UI.SetActive(true);
        yield return new WaitForSeconds(3);
        level5Manager.ReturnLevelState(returnState);
        warn_UI.SetActive(false);
        isTrigger = false;
    }

    IEnumerator UpdateLevelState(Level5State state)
    {
        yield return new WaitForSeconds(2);
        level5Manager.UpdateLevel5State(state);
        isTrigger = false;
    }
}
