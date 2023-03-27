using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] Level1Manager level1Manager;
    [SerializeField] float timeCurrentCD;
    float angle = 30;
    public int state;
    public GameObject windCollider;

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
                if(transform.rotation.eulerAngles.y >= angle && transform.rotation.eulerAngles.y < 180){
                    timeCurrentCD = 4;
                    state = 1;
                }
                break;
            case 1:
                if(transform.rotation.eulerAngles.y >= 360-angle){
                    timeCurrentCD = 3;
                    state = 2;
                }
                break;
            case 2:
                if(transform.rotation.eulerAngles.y >= angle && transform.rotation.eulerAngles.y < 180){
                    timeCurrentCD = 3;
                    state = 3;
                }
                break;
            case 3:
                if(transform.rotation.eulerAngles.y >= 360-angle){
                    timeCurrentCD = 2;
                    state = 4;
                }
                break;
            case 4:
                windCollider.SetActive(true);
                level1Manager.UpdateLevel1State(Level1State.Cover);
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Place"))
        {
            Destroy(other.gameObject);
            level1Manager.UpdateLevel1State(Level1State.Fan);
        }
    }
}
