using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag_PC : MonoBehaviour
{
    [SerializeField] Level5Manager_PC level5Manager;
    [SerializeField] GameObject steelWool;
    bool isTrigger;
    [SerializeField] GameObject warn_UI;
    public bool isBag2;
    [SerializeField] GameObject zipper;
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SteelWool") && !isTrigger)
        {
            other.gameObject.SetActive(false);
            steelWool.SetActive(true);
            zipper.SetActive(false);
            isTrigger = true;
            animator.SetBool("Trigger", true);
            if(isBag2){
                StartCoroutine(UpdateLevelState(Level5State_PC.Test));
            }else{
                StartCoroutine(UpdateLevelState(Level5State_PC.Vinegar));
            }
        }
    }

    IEnumerator UpdateLevelState(Level5State_PC state)
    {
        yield return new WaitForSeconds(8);
        level5Manager.UpdateLevel5State(state);
        isTrigger = false;
    }

    IEnumerator ReturnState(Level5State_PC returnState)
    {
        isTrigger = true;
        warn_UI.SetActive(true);
        yield return new WaitForSeconds(3);
        level5Manager.ReturnLevelState(returnState);
        warn_UI.SetActive(false);
        isTrigger = false;
    }
}
