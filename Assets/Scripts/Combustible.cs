using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combustible : MonoBehaviour
{
    [SerializeField] Level2Manager level2Manager;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Outside"))
        {
            level2Manager.combustiblesCount++;
            level2Manager.UpdateCombustiblesCount();
            Destroy(gameObject);
        }
    }
}
