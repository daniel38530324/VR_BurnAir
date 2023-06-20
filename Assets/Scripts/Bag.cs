using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag : MonoBehaviour
{
    [SerializeField] Level5Manager level5Manager;
    [SerializeField] GameObject steelWool;
    bool isTrigger;
    [SerializeField] GameObject warn_UI;
    public bool isBag2;
    [SerializeField] GameObject zipper;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SteelWool") && !isTrigger)
        {
            other.gameObject.SetActive(false);
            steelWool.SetActive(true);
            zipper.SetActive(false);
            isTrigger = true;
            if(isBag2){
                StartCoroutine(UpdateLevelState(Level5State.Test));
            }else{
                StartCoroutine(UpdateLevelState(Level5State.Vinegar));
            }
        }

        /*
        if (other.CompareTag("Vinegar") && !isTrigger)
        {
            if(!isBag2){
                StartCoroutine(ReturnState(Level5State.Vinegar));
            }
        }
        */
    }

    IEnumerator UpdateLevelState(Level5State state)
    {
        yield return new WaitForSeconds(8);
        level5Manager.UpdateLevel5State(state);
        isTrigger = false;
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
}
