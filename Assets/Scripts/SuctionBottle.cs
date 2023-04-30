using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuctionBottle : MonoBehaviour
{
    [SerializeField] Level3Manager level3Manager;
    [SerializeField] GameObject mnO2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("MnO2"))
        {
            StartCoroutine(MnO2());
        }
        else if (other.CompareTag("Water"))
        {
            StartCoroutine(Water());
        }
        else if (other.CompareTag("H2O2"))
        {
            StartCoroutine(H2O2());
        }
    }

    IEnumerator MnO2()
    {
        yield return new WaitForSeconds(2);
        mnO2.SetActive(true);
        level3Manager.UpdateLevel3State(Level3State.Water);
    }

    IEnumerator Water()
    {
        yield return new WaitForSeconds(2);
        level3Manager.UpdateLevel3State(Level3State.H2O2);
    }

    IEnumerator H2O2()
    {
        yield return new WaitForSeconds(2);
        level3Manager.UpdateLevel3State(Level3State.Tube);
    }
}
