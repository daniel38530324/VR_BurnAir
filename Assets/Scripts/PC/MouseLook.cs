using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform body;

    float xRotation = 0;

    public Camera cam;
    public LayerMask layerForRay;
    public Transform currentRayTransform;
    public Transform currentRayTransform_BeTrigger;
    float raydst = 10f;

    [Header("Button")]
    public Button continueBtn;
    public Button leftBtn;
    public Button rightBtn;
    public Button testBtn_forLV2;

    [Header("SetPos")]
    public string beCatchName = "";
    public Transform setPos;
    public Vector3 currentRayPoint;

    [Header("Level4")]
    public PlasticBag_Shake_PC plasticBag_Shake_PC;
    [Header("Level5")]
    public Transform clip;
    
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
        
        if(Input.GetKeyDown(KeyCode.Escape)){
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene("MainPage_PC");
        }

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
                //level4
                case "LimeWater_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = Vector3.zero;
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "LimeWater_interact";
                    }
                    break;
                case "Vinegar_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = Vector3.zero;
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "Vinegar_interact";
                    }
                    break;
                case "Soda_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = new Vector3(0f, 0.06f, 0f);
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "Soda_interact";
                    }
                    break;
                case "PlasticBag_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = Vector3.zero;
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "PlasticBag_interact";
                    }
                    break;
                case "PlasticBag_CO2_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = new Vector3(0.013f, 0.027f, 0.068f);
                        currentRayTransform.localRotation = Quaternion.Euler(0, 90f, 0);
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "PlasticBag_CO2_interact";
                    }
                    break;
                //level5
                case "Clip_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = Vector3.zero;
                        currentRayTransform.localRotation = Quaternion.Euler(0, 180f, 0);
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "Clip_interact";
                    }
                    break;
                case "Petri_Dish_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = Vector3.zero;
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "Petri_Dish_interact";
                    }
                    break;
                case "water_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = Vector3.zero;
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "water_interact";
                    }
                    break;
                    //beTrigger
                case "Steel_Wool_BeTrigger":
                    if(beCatchName == "Clip_interact"){
                        currentRayTransform.SetParent(clip);
                        currentRayTransform.localPosition = Vector3.zero;
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "Steel_Wool_BeTrigger";
                    }
                    break;
                //level6
                case "WD40_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = new Vector3(0, -0.015f, 0);
                        currentRayTransform.localRotation = Quaternion.Euler(0, -120f, 0);
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "WD40_interact";
                    }
                    break;
                case "PaintGun_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = new Vector3(-0.015f, 0.08f, 0.03f);
                        currentRayTransform.localRotation = Quaternion.Euler(0, -115f, 0);
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "PaintGun_interact";
                    }
                    break;
                case "Rag_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = Vector3.zero;
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "Rag_interact";
                    }
                    break;
                case "Lemonade_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = new Vector3(0, -0.02f, 0);
                        currentRayTransform.localRotation = Quaternion.Euler(0, -90f, 0);
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "Lemonade_interact";
                    }
                    break;
                case "PlasticSleeve_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = Vector3.zero;
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "PlasticSleeve_interact";
                    }
                    break;
                //new other
                case "BeakerTongs_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = new Vector3(-0.007f, 0f, 0.184f);
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "BeakerTongs_interact";
                    }
                    break;
                case "Forceps_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = new Vector3(-0.079f, 0, 0.129f);
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "Forceps_interact";
                    }
                    break;
                case "Pestle_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = new Vector3(0, 0.014f, 0.13f);
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "Pestle_interact";
                    }
                    break;
                case "TestTubes_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = new Vector3(0.064f, -0.121f, 0.164f);
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "TestTubes_interact";
                    }
                    break;
                case "WireGauze_interact":
                    if(beCatchName == ""){
                        currentRayTransform.SetParent(setPos);
                        currentRayTransform.localPosition = new Vector3(-0.106f, -0.083f, 0.164f);
                        currentRayTransform.localRotation = Quaternion.identity;
                        currentRayTransform.GetComponent<Rigidbody>().isKinematic = true;
                        beCatchName = "WireGauze_interact";
                    }
                    break;
                //other
                default:
                    if(beCatchName == "Steel_Wool_BeTrigger"){
                        clip.transform.GetComponentInChildren<Rigidbody>().isKinematic = false;
                        clip.GetChild(0).position = currentRayPoint + new Vector3(0,0f,0);
                        clip.GetChild(0).SetParent(null);
                        beCatchName = "Clip_interact";
                    }else if(beCatchName != ""){
                        PutThingDown();
                    }
                    break;
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
                case "Soda_interact":
                    setPos.transform.GetComponentInChildren<Animator>().enabled = true;
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", true);
                    break;
                case "Vinegar_interact":
                    setPos.transform.GetComponentInChildren<Animator>().enabled = true;
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", true);
                    break;
                case "LimeWater_interact":
                    setPos.transform.GetComponentInChildren<Animator>().enabled = true;
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", true);
                    break;
                case "PlasticBag_CO2_interact":
                    setPos.transform.GetComponentInChildren<Animator>().enabled = true;
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", true);
                    if(SceneManager.GetActiveScene().name == "Level4_PC"){
                        plasticBag_Shake_PC.Shake();
                    }
                    break;
                case "water_interact":
                    setPos.transform.GetComponentInChildren<Animator>().enabled = true;
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", true);
                    break;
                case "Lemonade_interact":
                    setPos.transform.GetComponentInChildren<Animator>().enabled = true;
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", true);
                    break;
                case "Rag_interact":
                    setPos.transform.GetComponentInChildren<Animator>().enabled = true;
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", true);
                    break;
                case "WD40_interact":
                    setPos.transform.GetComponentInChildren<Animator>().enabled = true;
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", true);
                    break;
                case "PaintGun_interact":
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
                case "Soda_interact":
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", false);
                    break;
                case "Vinegar_interact":
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", false);
                    break;
                case "LimeWater_interact":
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", false);
                    break;
                case "PlasticBag_CO2_interact":
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", false);
                    break;
                case "water_interact":
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", false);
                    break;
                case "Lemonade_interact":
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", false);
                    break;
                case "Rag_interact":
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", false);
                    break;
                case "WD40_interact":
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", false);
                    break;
                case "PaintGun_interact":
                    setPos.transform.GetComponentInChildren<Animator>().SetBool("Trigger", false);
                    break;
            }
        }
    }

    void Ray()
    {
        currentRayTransform = null;
        RaycastHit hit;
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, raydst, layerForRay)){
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
            case "Soda_interact":
                if(!setPos.transform.GetComponentInChildren<Animator_Trigger>().trigger){
                    setPos.transform.GetComponentInChildren<Animator>().enabled = false;
                    setPos.transform.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    setPos.GetChild(0).position = currentRayPoint + new Vector3(0,0.2f,0);
                    setPos.GetChild(0).SetParent(null);
                    beCatchName = "";
                }
                break;
            case "Vinegar_interact":
                if(!setPos.transform.GetComponentInChildren<Animator_Trigger>().trigger){
                    setPos.transform.GetComponentInChildren<Animator>().enabled = false;
                    setPos.transform.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    setPos.GetChild(0).position = currentRayPoint + new Vector3(0,0.2f,0);
                    setPos.GetChild(0).SetParent(null);
                    beCatchName = "";
                }
                break;
            case "LimeWater_interact":
                if(!setPos.transform.GetComponentInChildren<Animator_Trigger>().trigger){
                    setPos.transform.GetComponentInChildren<Animator>().enabled = false;
                    setPos.transform.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    setPos.GetChild(0).position = currentRayPoint + new Vector3(0,0.2f,0);
                    setPos.GetChild(0).SetParent(null);
                    beCatchName = "";
                }
                break;
            case "PlasticBag_CO2_interact":
                if(!setPos.transform.GetComponentInChildren<Animator_Trigger>().trigger){
                    setPos.transform.GetComponentInChildren<Animator>().enabled = false;
                    setPos.transform.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    setPos.GetChild(0).position = currentRayPoint + new Vector3(0,0.2f,0);
                    setPos.GetChild(0).SetParent(null);
                    beCatchName = "";
                }
                break;
            case "water_interact":
                if(!setPos.transform.GetComponentInChildren<Animator_Trigger>().trigger){
                    setPos.transform.GetComponentInChildren<Animator>().enabled = false;
                    setPos.transform.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    setPos.GetChild(0).position = currentRayPoint + new Vector3(0,0.2f,0);
                    setPos.GetChild(0).SetParent(null);
                    beCatchName = "";
                }
                break;
            case "Lemonade_interact":
                if(!setPos.transform.GetComponentInChildren<Animator_Trigger>().trigger){
                    setPos.transform.GetComponentInChildren<Animator>().enabled = false;
                    setPos.transform.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    setPos.GetChild(0).position = currentRayPoint + new Vector3(0,0.2f,0);
                    setPos.GetChild(0).SetParent(null);
                    beCatchName = "";
                }
                break;
            case "Rag_interact":
                if(!setPos.transform.GetComponentInChildren<Animator_Trigger>().trigger){
                    setPos.transform.GetComponentInChildren<Animator>().enabled = false;
                    setPos.transform.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    setPos.GetChild(0).position = currentRayPoint + new Vector3(0,0.2f,0);
                    setPos.GetChild(0).SetParent(null);
                    beCatchName = "";
                }
                break;
            case "WD40_interact":
                if(!setPos.transform.GetComponentInChildren<Animator_Trigger>().trigger){
                    setPos.transform.GetComponentInChildren<Animator>().enabled = false;
                    setPos.transform.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    setPos.GetChild(0).position = currentRayPoint + new Vector3(0,0.2f,0);
                    setPos.GetChild(0).SetParent(null);
                    beCatchName = "";
                }
                break;
            case "PaintGun_interact":
                if(!setPos.transform.GetComponentInChildren<Animator_Trigger>().trigger){
                    setPos.transform.GetComponentInChildren<Animator>().enabled = false;
                    setPos.transform.GetComponentInChildren<Rigidbody>().isKinematic = false;
                    setPos.GetChild(0).position = currentRayPoint + new Vector3(0,0.2f,0);
                    setPos.GetChild(0).SetParent(null);
                    beCatchName = "";
                }
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
