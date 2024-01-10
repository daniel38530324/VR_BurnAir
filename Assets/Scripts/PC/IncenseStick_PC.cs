using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IncenseStick_PC : MonoBehaviour
{
    public Level3Manager_PC level3Manager;
    bool trigger;
    public GameObject fire, smoke;
    [SerializeField] bool isLevel4;

    [SerializeField] UnityEvent UpdateLevelState_Test;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wind") && !trigger){
            trigger = true;
            StartCoroutine(Finish());
        }
    }

    IEnumerator Finish()
    {
        if(!isLevel4)
        {
            //fire anim
            fire.SetActive(true);
        }
        else
        {
            smoke.SetActive(false);
        }
        yield return new WaitForSeconds(4f);
        UpdateLevelState_Test.Invoke();
        //level3Manager.UpdateLevel3State(Level3State.Test);
    }
}
