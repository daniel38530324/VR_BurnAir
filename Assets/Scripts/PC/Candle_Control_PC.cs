using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candle_Control_PC : MonoBehaviour
{
    [SerializeField] GameObject warn_UI;
    public bool isTrigger;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Wind"))
        {
            StartCoroutine(Warn());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cover") && !isTrigger)
        {
            StartCoroutine(Warn());
        }

        if (other.CompareTag("Water") && !isTrigger)
        {
            StartCoroutine(Warn());
        }

        if (other.CompareTag("Flour") && !isTrigger)
        {
            StartCoroutine(Warn());
        }
    }

    IEnumerator Warn()
    {
        isTrigger = true;
        warn_UI.SetActive(true);
        yield return new WaitForSeconds(3);
        warn_UI.SetActive(false);
        isTrigger = false;
    }
}
