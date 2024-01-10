using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combustible_PC : MonoBehaviour
{
    [SerializeField] Level2Manager_PC level2Manager;
    bool beenused;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Outside") && !beenused)
        {
            beenused = true;
            level2Manager.combustiblesCount++;
            level2Manager.UpdateCombustiblesCount();
            Destroy(gameObject);
        }
    }
}
