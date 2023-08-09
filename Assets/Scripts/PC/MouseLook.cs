using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform body;

    float xRotation = 0;

    public Camera cam;
    public LayerMask layerForRay;
    public Transform currentRayTransform;

    [Header("Button")]
    public Button continueBtn;
    public Button leftBtn;
    public Button rightBtn;
    public Button testBtn_forLV2;

    [Header("SetPos")]
    public string beCatchName = "";
    public Transform setPos;
    public Vector3 currentRayPoint;
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 70f);

        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        body.Rotate(Vector3.up * mouseX);

        Ray();

        if(Input.GetMouseButtonDown(0) && currentRayTransform != null){
            switch (currentRayTransform.name)
            {
                //btn
                case "UI_Continue":
                    continueBtn.onClick.Invoke();
                    break;
                case "UI_Left":
                    leftBtn.onClick.Invoke();
                    break;
                case "UI_Right":
                    rightBtn.onClick.Invoke();
                    break;
                case "UI_Test":
                    testBtn_forLV2.onClick.Invoke();
                    break;
                //level1
                case "H2O2_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = Vector3.zero;
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "H2O2_interact";
                    }
                    break;
                case "MnO2_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = Vector3.zero;
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "MnO2_interact";
                    }
                    break;
                case "WaterBucket_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = new Vector3(0.05f, -0.15f, 0.167f);
                        currentRayTransform.localRotation = Quaternion.Euler(0, 0, 5);
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "WaterBucket_interact";
                    }
                    break;
                case "Flour_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = new Vector3(0.05f, -0.16f, 0.12f);
                        currentRayTransform.localRotation = Quaternion.Euler(0, -35, 0);
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "Flour_interact";
                    }
                    break;
                case "Bottle_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = Vector3.zero;
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "Bottle_interact";
                    }
                    break;
                case "handfan02_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = new Vector3(0, 0, 0.12f);
                        currentRayTransform.localRotation = Quaternion.Euler(0,-180,0);
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "handfan02_interact";
                    }
                    break;
                //level2
                case "AlcoholLamp_Cover_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = Vector3.zero;
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "AlcoholLamp_Cover_interact";
                    }
                    break;
                case "FireEx_DryPowde_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = new Vector3(0.073f, -0.45f, 0.175f);
                        currentRayTransform.localRotation = Quaternion.Euler(0,0.34f,0);
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "FireEx_DryPowde_interact";
                    }
                    break;
                case "FireEx_Metal_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = new Vector3(0.073f, -0.45f, 0.175f);
                        currentRayTransform.localRotation = Quaternion.Euler(0,0.34f,0);
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "FireEx_Metal_interact";
                    }
                    break;
                case "Combustible_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = Vector3.zero;
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "Combustible_interact";
                    }
                    break;
                case "Door_interact":
                    bool t = currentRayTransform.GetComponentInChildren<Animator_Trigger>().SetTriggerReverse();
                    currentRayTransform.GetComponentInChildren<Animator>().SetBool("Trigger", t);
                    break;
                //level3
                case "Mushroom_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = new Vector3(0f, 0f, 0.13f);
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "Mushroom_interact";
                    }
                    break;
                case "Cover_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = Vector3.zero;
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "Cover_interact";
                    }
                    break;
                case "GlassCover_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = Vector3.zero;
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "GlassCover_interact";
                    }
                    break;
                case "IncenseSticks_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = new Vector3(0f, 0.03f, 0.26f);
                        currentRayTransform.localRotation = Quaternion.Euler(100, 141.3f, 160);
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "IncenseSticks_interact";
                    }
                    break;
                //other
                default:
                    if(beCatchName != ""){
                        PutThingDown();
                    }
                    break;
                /*case "Trigger_interact":
                    if(beCatchName != ""){
                        PutThingDown();
                        beCatchName = "";
                    }
                    break;*/
            }
        }

        if(Input.GetMouseButtonDown(1)){
            switch (beCatchName)
            {
                case "handfan02_interact":
                    setPos.transform.GetComponentInChildren<Animator>().enabled = true;
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", true);
                    break;
                case "WaterBucket_interact":
                    setPos.transform.GetComponentInChildren<Animator>().enabled = true;
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", true);
                    break;
                case "Flour_interact":
                    setPos.transform.GetComponentInChildren<Animator>().enabled = true;
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", true);
                    break;
                case "FireEx_DryPowde_interact":
                    setPos.transform.GetComponentInChildren<Animator>().enabled = true;
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", true);
                    break;
                case "FireEx_Metal_interact":
                    setPos.transform.GetComponentInChildren<Animator>().enabled = true;
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", true);
                    break;
                case "Mushroom_interact":
                    setPos.transform.GetComponentInChildren<Animator>().enabled = true;
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", true);
                    break;
                case "H2O2_interact":
                    setPos.transform.GetComponentInChildren<Animator>().enabled = true;
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", true);
                    break;
                case "IncenseSticks_interact":
                    setPos.transform.GetComponentInChildren<Animator>().enabled = true;
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", true);
                    break;
            }
        }
        if(Input.GetMouseButtonUp(1)){
            switch (beCatchName)
            {
                case "handfan02_interact":
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", false);
                    break;
                case "WaterBucket_interact":
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", false);
                    break;
                case "Flour_interact":
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", false);
                    break;
                case "FireEx_DryPowde_interact":
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", false);
                    break;
                case "FireEx_Metal_interact":
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", false);
                    break;
                case "Mushroom_interact":
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", false);
                    break;
                case "H2O2_interact":
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", false);
                    break;
                case "IncenseSticks_interact":
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", false);
                    break;
            }
        }
    }

    void Ray()
    {
        currentRayTransform = null;
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Mathf.Infinity, layerForRay)){
            currentRayTransform = hit.transform;
            currentRayPoint = hit.point;
        }
    }

    void PutThingDown()
    {
        switch (beCatchName)
        {
            case "handfan02_interact":
                if(!setPos.transform.GetComponentInChildren<Animator_Trigger>().trigger){
                    setPos.transform.GetComponentInChildren<Animator>().enabled = false;
                    setPos.transform.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    setPos.GetChild(0).position = currentRayPoint + new Vector3(0,0.2f,0);
                    setPos.GetChild(0).SetParent(null);
                    beCatchName = "";
                }
                break;
            case "WaterBucket_interact":
                if(!setPos.transform.GetComponentInChildren<Animator_Trigger>().trigger){
                    setPos.transform.GetComponentInChildren<Animator>().enabled = false;
                    setPos.transform.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    setPos.GetChild(0).position = currentRayPoint + new Vector3(0,0.2f,0);
                    setPos.GetChild(0).SetParent(null);
                    beCatchName = "";
                }
                break;
            case "Bottle_interact":
                    setPos.transform.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    setPos.GetChild(0).position = currentRayPoint + new Vector3(0,0.2f,0);
                    setPos.GetChild(0).rotation = Quaternion.Euler(0,0,-180);
                    setPos.GetChild(0).SetParent(null);
                    beCatchName = "";
                break;
            case "Mushroom_interact":
                if(!setPos.transform.GetComponentInChildren<Animator_Trigger>().trigger){
                    setPos.transform.GetComponentInChildren<Animator>().enabled = false;
                    setPos.transform.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    setPos.GetChild(0).position = currentRayPoint + new Vector3(0,0.2f,0);
                    setPos.GetChild(0).SetParent(null);
                    beCatchName = "";
                }
                break;
            case "Flour_interact":
                if(!setPos.transform.GetComponentInChildren<Animator_Trigger>().trigger){
                    setPos.transform.GetComponentInChildren<Animator>().enabled = false;
                    setPos.transform.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    setPos.GetChild(0).position = currentRayPoint + new Vector3(0,0.2f,0);
                    setPos.GetChild(0).SetParent(null);
                    beCatchName = "";
                }
                break;
            case "H2O2_interact":
                if(!setPos.transform.GetComponentInChildren<Animator_Trigger>().trigger){
                    setPos.transform.GetComponentInChildren<Animator>().enabled = false;
                    setPos.transform.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    setPos.GetChild(0).position = currentRayPoint + new Vector3(0,0.2f,0);
                    setPos.GetChild(0).SetParent(null);
                    beCatchName = "";
                }
                break;
            case "IncenseSticks_interact":
                if(!setPos.transform.GetComponentInChildren<Animator_Trigger>().trigger){
                    setPos.transform.GetComponentInChildren<Animator>().enabled = false;
                    setPos.transform.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    setPos.GetChild(0).position = currentRayPoint + new Vector3(0,0.2f,0);
                    setPos.GetChild(0).SetParent(null);
                    beCatchName = "";
                }
                break;
            case "GlassCover_interact":
                setPos.transform.GetComponentInChildren<Rigidbody>().isKinematic = false;
                setPos.GetChild(0).position = currentRayPoint;
                setPos.GetChild(0).SetParent(null);
                beCatchName = "";
                break;
            default:
                setPos.transform.GetComponentInChildren<Rigidbody>().isKinematic = false;
                setPos.GetChild(0).position = currentRayPoint + new Vector3(0,0.2f,0);
                setPos.GetChild(0).SetParent(null);
                beCatchName = "";
                break;
        }
    }

    public void RemoveThingOnHand()
    {
        if(beCatchName != ""){
            setPos.GetChild(0).SetParent(null);
            beCatchName = "";
        }
    }
}
