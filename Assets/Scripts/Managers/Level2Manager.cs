using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum Level2State
{
    Explain,      //說明階段
    Combustibles, //易燃物階段
    Fire,         //滅火階段
    Success,      //成功階段
    Fail          //失敗階段
}

public class Level2Manager : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameObject gameManager;

    [Header("Mission")]
    [SerializeField] Text mission_Text;
    [SerializeField] Text timer_Text;

    [Header("Object")]
    [SerializeField] GameObject fires;
    [SerializeField] GameObject combustibles, extinguishingTools;

    
    [NonSerialized] public int combustiblesCount = 0, fireCount = 0;
    Level2State level2State;
    float timer = 90;
    bool timerState;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        UpdateLevel2State(Level2State.Explain);
    }

    private void Update()
    {
        if(timerState)
        {
            timer -= Time.deltaTime;
            timer_Text.text = timer.ToString("0");

            if (timer <= 0)
                UpdateLevel2State(Level2State.Fail);
        }
    }

    public void UpdateLevel2State(Level2State newState)
    {
        level2State = newState;

        switch(newState)
        {
            case Level2State.Explain:
                break;
            case Level2State.Combustibles:
                mission_Text.transform.parent.gameObject.SetActive(true);
                UpdateCombustiblesCount();
                combustibles.SetActive(true);
                fires.SetActive(true);
                timerState = true;
                break;
            case Level2State.Fire:
                timer = 120;
                UpdateFireCount();
                extinguishingTools.SetActive(true);
                break;
            case Level2State.Success:
                timerState = false;
                mission_Text.transform.parent.gameObject.SetActive(false);
                break;
            case Level2State.Fail:
                timerState = false;
                mission_Text.transform.parent.gameObject.SetActive(false);
                break;
        }
    }

    public void UpdateLevel2State_Int(int newState)
    {
        UpdateLevel2State((Level2State)newState);
    }

    public void UpdateCombustiblesCount()
    {
        mission_Text.text = "移除易燃物:" + combustiblesCount + "/4";
        if(combustiblesCount >= 4)
        {
            UpdateLevel2State(Level2State.Fire);
        }
    }

    public void UpdateFireCount()
    {
        mission_Text.text = "滅掉火源:" + fireCount + "/4";
        if(fireCount >= 4)
        {
            UpdateLevel2State(Level2State.Success);
        }
    }
}