using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle : MonoBehaviour
{
    [SerializeField] Level1Manager level1Manager;
    public bool haveWind;
    public ParticleSystem particleSystem;

    private void Update()
    {
        if(haveWind){
            StartCoroutine(Burn());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Wind")){
            haveWind = true;
        }
    }

    IEnumerator Burn()
    {
        var main = particleSystem.main;
        main.startSize = 0.1f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0.2f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0.3f;
        yield return new WaitForSeconds(0.2f);
        main.startSize = 0.4f;
        yield return new WaitForSeconds(3f);
        level1Manager.UpdateLevel1State(Level1State.Cover);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Cover")){
            var main = particleSystem.main;
            main.startSize = 0f;
        }
    }
}
