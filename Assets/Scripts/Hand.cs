using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Hand : MonoBehaviour
{
    public GameObject handPrefab; 
    public InputDeviceCharacteristics inputDeviceCharacteristics; 
    public bool hideHandOnSelect = false;

    private InputDevice _tragetDevice;
    private Animator _handAnimator;
    private SkinnedMeshRenderer _hashMesh;
    
    public void HideHandOnSelect()
    {
        if(hideHandOnSelect)
        {
            _hashMesh.enabled = !_hashMesh.enabled; 
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        initializeHand();
    }

    void initializeHand()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(inputDeviceCharacteristics, devices); 

        if(devices.Count > 0) 
        {
            _tragetDevice = devices[0];

            GameObject spawnHand = Instantiate(handPrefab, transform);
            _handAnimator = spawnHand.GetComponent<Animator>();
            _hashMesh = spawnHand.GetComponentInChildren<SkinnedMeshRenderer>(); 
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!_tragetDevice.isValid) 
        {
            initializeHand();
        }
        else
        {
            updateHand();
        }
    }

    void updateHand()
    {
        if(_tragetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue)) 
        {
            _handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            _handAnimator.SetFloat("Trigger", 0);
        }

        if(_tragetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            _handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            _handAnimator.SetFloat("Grip", 0);
        }
    }
}
