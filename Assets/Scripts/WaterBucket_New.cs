using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBucket_New : MonoBehaviour
{
    [SerializeField] GameObject waterEffect, water;
    public Level1Manager levelManager;
    public Level2Manager level2Manager;
    public Level3Manager level3Manager;
    public Level4Manager level4Manager;
    public GameObject warn_UI;
    public int level;

    public bool isBucket;
    bool trigger = true;
    float returnCD = 4f;
    public bool isReturn;

    [Header("Level1")]
    public Candle_Control candle_Control;
    public Candle candle;
    
    [Header("level2")]
    public Fire fire;

    [Header("level3 and level4")]
    public bool isMno2;
    public SuctionBottle suctionBottle;
    public bool isCaCO3;

    private void OnEnable()
    {
        trigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!trigger)
        {
            //Debug.Log(transform.eulerAngles.x);
            if (Quaternion.Angle(transform.rotation, Quaternion.identity) >= 160)
            {
                if(waterEffect)
                {
                    waterEffect.SetActive(true);
                }
                if(water)
                {
                    water.SetActive(false);
                }
               
                trigger = true;
            }
        }else{
            returnCD -= Time.deltaTime;

            if(returnCD <= 0){
                if(level == 1){
                    if(levelManager.level1State == Level1State.Bucket && isBucket && !isReturn && !candle.isTrigger2){
                        StartCoroutine(ReturnState_level1(Level1State.Bucket));
                    }else if(levelManager.level1State == Level1State.Flour && !isBucket && !isReturn){
                        StartCoroutine(ReturnState_level1(Level1State.Flour));
                    }
                }else if(level == 2){
                    if(level2Manager.level2State == Level2State.Fire && !isReturn && !fire.beenused){
                        StartCoroutine(ReturnState_level2(Level2State.Fire));
                    }
                }else if(level == 3){
                    if(level3Manager.level3State == Level3State.MnO2 && isMno2 && !isReturn && !suctionBottle.isTrigger){
                        StartCoroutine(ReturnState_level3(Level3State.MnO2));
                    }else if(level3Manager.level3State == Level3State.Water && !isMno2 && isBucket && !isReturn && !suctionBottle.isTrigger2){
                        StartCoroutine(ReturnState_level3(Level3State.Water));
                    }else if(level3Manager.level3State == Level3State.H2O2 && !isMno2 && !isBucket && !isReturn && !suctionBottle.isTrigger3){
                        StartCoroutine(ReturnState_level3(Level3State.H2O2));
                    }
                }else if(level == 4){
                    if(level4Manager.level4State == Level4State.CaCO3 && isCaCO3 && !isReturn && !suctionBottle.isTrigger){
                        StartCoroutine(ReturnState_level4(Level4State.CaCO3));
                    }else if(level4Manager.level4State == Level4State.Water && isBucket && !isReturn && !suctionBottle.isTrigger2){
                        StartCoroutine(ReturnState_level4(Level4State.Water));
                    }else if(level4Manager.level4State == Level4State.HCl && !isBucket && !isReturn && !suctionBottle.isTrigger3){
                        StartCoroutine(ReturnState_level4(Level4State.HCl));
                    }
                }
            }
        }
    }

    IEnumerator ReturnState_level1(Level1State state)
    {
        if(!candle_Control.isTrigger){//檢查玩家是否是在對照組進行操作
            isReturn = true;
            if(waterEffect)
            {
                waterEffect.SetActive(false);
            }
            warn_UI.SetActive(true);

            yield return new WaitForSeconds(4f);
            levelManager.ReturnLevelState(state);
            warn_UI.SetActive(false);

            returnCD = 4f;
            isReturn = false;
            if(water)
            {
                water.SetActive(true);
            }
            waterEffect.SetActive(false);
        }
    }

    IEnumerator ReturnState_level2(Level2State state)
    {
        isReturn = true;
        if(waterEffect)
        {
            waterEffect.SetActive(false);
        }
        warn_UI.SetActive(true);

        yield return new WaitForSeconds(4f);
        level2Manager.ReturnLevelState(state);
        warn_UI.SetActive(false);

        returnCD = 4f;
        isReturn = false;
        if(water)
        {
            water.SetActive(true);
        }
        waterEffect.SetActive(false);
    }

    IEnumerator ReturnState_level3(Level3State state)
    {
        isReturn = true;
        if(waterEffect)
        {
            waterEffect.SetActive(false);
        }
        warn_UI.SetActive(true);

        yield return new WaitForSeconds(4f);
        level3Manager.ReturnLevelState(state);
        warn_UI.SetActive(false);

        returnCD = 4f;
        isReturn = false;
        if(water)
        {
            water.SetActive(true);
        }
        waterEffect.SetActive(false);
    }

    IEnumerator ReturnState_level4(Level4State state)
    {
        isReturn = true;
        if(waterEffect)
        {
            waterEffect.SetActive(false);
        }
        warn_UI.SetActive(true);

        yield return new WaitForSeconds(4f);
        level4Manager.ReturnLevelState(state);
        warn_UI.SetActive(false);

        returnCD = 4f;
        isReturn = false;
        if(water)
        {
            water.SetActive(true);
        }
        waterEffect.SetActive(false);
    }
}
