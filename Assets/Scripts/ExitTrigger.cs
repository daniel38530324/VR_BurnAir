using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    [SerializeField] TutorialManager tutorialManager;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("TutorialExit"))
        {
            tutorialManager.ReturnMainPage();
        }
    }
}
