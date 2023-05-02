using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    public Level3Manager level3Manager;
    public GameObject pipe;
    public GameObject original_pipe;
    bool isTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Water") && !isTrigger){
            StartCoroutine(Finish());
        }
    }
    
    IEnumerator Finish()
    {
        isTrigger = true;
        yield return new WaitForSeconds(1f);
        pipe.SetActive(true);
        original_pipe.SetActive(false);
        level3Manager.UpdateLevel3State(Level3State.Cover);

    }
}
