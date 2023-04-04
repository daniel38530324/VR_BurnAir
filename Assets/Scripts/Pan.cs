using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : MonoBehaviour
{
    public bool isTrigger;
    public ParticleSystem particleSystem;
    public GameObject[] fireEffects;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Cover") && !isTrigger){
            StartCoroutine(BeenCover());
        }
    }

    IEnumerator BeenCover()
    {
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
