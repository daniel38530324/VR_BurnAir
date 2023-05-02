using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public bool inWater;
    public Level3Manager level3Manager;
    [Header("如果放在水裡才要放")]
    public GameObject bubble;

    [Header("如果沒放在水裡才要放")]
    public MeshRenderer meshRenderer;
    public GameObject bottleInWater;
    public BoxCollider boxCollider;

    bool isTrigger;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Water") && !isTrigger && !inWater){
            StartCoroutine(Finish());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Water") && !isTrigger && inWater){
            StartCoroutine(Finish2());
        }
    }

    IEnumerator Finish()
    {
        isTrigger = true;
        yield return new WaitForSeconds(1f);
        meshRenderer.enabled = false;
        boxCollider.enabled = false;
        bottleInWater.SetActive(true);
        yield return new WaitForSeconds(4f);
        level3Manager.UpdateLevel3State(Level3State.PickUp);
    }

    IEnumerator Finish2()
    {
        isTrigger = true;
        bubble.SetActive(false);
        yield return new WaitForSeconds(2f);
        level3Manager.UpdateLevel3State(Level3State.IncenseSticks);
        Destroy(gameObject);
    }
}