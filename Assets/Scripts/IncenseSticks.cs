using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncenseSticks : MonoBehaviour
{
    public Level3Manager level3Manager;
    bool trigger;
    public GameObject fire;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Wind") && !trigger){
            trigger = true;
            StartCoroutine(Finish());
        }
    }

    IEnumerator Finish()
    {
        //fire anim
        fire.SetActive(true);
        yield return new WaitForSeconds(4f);
        level3Manager.UpdateLevel3State(Level3State.Test);
    }
}
