using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private Vector3 myScale;
    private float durationTime = 5, timer = 0;
    private bool turnOff;

    private void Start()
    {
        myScale = transform.localScale;
    }

    private void Update()
    {
        if (turnOff)
        {
            if (timer < durationTime)
            {
                timer += Time.deltaTime;
                transform.localScale = Vector3.Lerp(myScale, Vector3.zero, timer / durationTime);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Bubble"))
        {
            
            turnOff = true;
            
        }
    }
}
