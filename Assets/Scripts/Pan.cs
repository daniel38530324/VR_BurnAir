using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour
{
    public Level2Manager level2Manager;
    public bool isTrigger;
    public ParticleSystem particleSystem;
    public GameObject[] fireEffects;
    public GameObject anotherCover;
    public GameObject myCover;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Cover") && !isTrigger){
            anotherCover.SetActive(false);
            myCover.SetActive(true);
            StartCoroutine(BeenCover());
        }
    }

    IEnumerator BeenCover()
    {
        level2Manager.fireCount++;
        level2Manager.UpdateFireCount();
        level2Manager.GetKnowledgePoints(level2Manager.AlcoholLamp_UI, true);
        isTrigger = true;
        for (int i = 0; i < fireEffects.Length; i++)
        {
           fireEffects[i].SetActive(false); 
        }
        var main = particleSystem.main;
        main.startSize = 0.4f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0.3f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0.2f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0.1f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0f;
    }
}
