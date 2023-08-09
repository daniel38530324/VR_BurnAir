using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animator_Trigger : MonoBehaviour
{
    public bool trigger;

    public bool SetTriggerReverse(){
        trigger = !trigger;
        return trigger;
    }
}
