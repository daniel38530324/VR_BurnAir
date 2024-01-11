using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan_PC : MonoBehaviour
{
    public Level2Manager_PC level2Manager;
    public bool isTrigger;
    public ParticleSystem particleSystem;
    public GameObject[] fireEffects;
    public GameObject anotherCover;
    public GameObject myCover;
    [SerializeField] AudioSource fireSound;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Cover") && !isTrigger){
            anotherCover.SetActive(false);
            myCover.SetActive(true);
            StartCoroutine(BeenCover());
            level2Manager.CheckFinish(1);
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
        fireSound.volume = 0.8f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0.3f;
        fireSound.volume = 0.6f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0.2f;
        fireSound.volume = 0.4f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0.1f;
        fireSound.volume = 0.2f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0f;
        fireSound.volume = 0f;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bubble") || other.gameObject.CompareTag("Metal") || other.gameObject.CompareTag("Water"))
        {
            level2Manager.GetWrong();
        }
    }

}
