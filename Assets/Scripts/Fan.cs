using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] Level1Manager level1Manager;
    [SerializeField] float timeCurrentCD;
    public float angle = 10;
    public int state;
    public GameObject windCollider;
    public ParticleSystem wind;

    private void Update()
    {
        if(timeCurrentCD <= 0){
            state = 0;
            windCollider.SetActive(false);
        }else{
            timeCurrentCD -= Time.deltaTime;
        }
        
        switch (state)
        {
            case 0:
                if(transform.localRotation.eulerAngles.z >= angle && transform.localRotation.eulerAngles.z < 180){
                    timeCurrentCD = 4;
                    state = 1;
                }
                break;
            case 1:
                if(transform.localRotation.eulerAngles.z >= 360-angle){
                    timeCurrentCD = 3;
                    state = 2;
                }
                break;
            case 2:
                if(transform.localRotation.eulerAngles.z >= angle && transform.localRotation.eulerAngles.z < 180){
                    timeCurrentCD = 3;
                    state = 3;
                    wind.Play();
                }
                break;
            case 3:
                if(transform.localRotation.eulerAngles.z >= 360-angle){
                    timeCurrentCD = 3;
                    state = 4;
                    wind.Play();
                }
                break;
            case 4:
                if(transform.localRotation.eulerAngles.z >= angle && transform.localRotation.eulerAngles.z < 180){
                    timeCurrentCD = 2;
                    state = 5;
                    wind.Play();
                }
                break;
            case 5:
                windCollider.SetActive(true);
                wind.Play();
                break;
        }
    }
    
}
