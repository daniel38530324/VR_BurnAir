using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] Level1Manager level1Manager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Place"))
        {
            Destroy(other.gameObject);
            level1Manager.UpdateLevel1State(Level1State.Fan);
        }
    }
}
