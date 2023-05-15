using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCover : MonoBehaviour
{
    public int level;
    public Level3Manager level3Manager;
    public Level4Manager level4Manager;
    bool trigger;
    public GameObject oxygenCollider;
    public bool inWater;

    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Cover") && !trigger && !inWater){
            trigger = true;
            oxygenCollider.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Water") && !trigger && inWater){
            StartCoroutine(Finish());
        }
    }

    IEnumerator Finish()
    {
        trigger = true;
        yield return new WaitForSeconds(1f);
        if(level == 3){
            level3Manager.UpdateLevel3State(Level3State.PickUp);
        }else if(level == 4){
            level4Manager.UpdateLevel4State(Level4State.PickUp);
        }
    }
}
