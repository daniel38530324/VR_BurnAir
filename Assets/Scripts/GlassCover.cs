using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassCover : MonoBehaviour
{
    bool trigger;
    public GameObject oxygenCollider;

    private void OnCollisionExit(Collision other)
    {
        if(other.gameObject.CompareTag("Cover") && !trigger){
            trigger = true;
            oxygenCollider.SetActive(true);
        }
    }
}
