using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public enum Level5State
{
    Explain,            //說明階段
    Choose,             //選擇器材階段
    Place,              //空氣-放置鋼棉階段
    Water,              //水-加入水階段
    Bag1,               //水-放進袋子階段
    Vinegar,            //醋-加入醋階段
    Bag2,               //醋-放進袋子階段
    Test,               //測驗階段
}


public class Level5Manager : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameObject gameManager;

    [Header("LearningProcess")]
    [SerializeField] LearningProcess learningProcess;

    [Header("Mission")]
    [SerializeField] Text mission_Text;

    [Header("Knowledge points")]
    [SerializeField] GameObject hint;
    [SerializeField] GameObject choose_UI, place_UI, water_UI, bag1_UI, vinegar_UI, bag2_UI;
    Level5State level5State;

    [Header("Object")]
    [SerializeField] GameObject table;
    [SerializeField] GameObject clip, vinegar, water, petriDish, steelWool, steelWool_control, steelWool_test;
    [SerializeField] Transform spawnPoint, spawnPoint2;

    float levelTimer = 0;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        UpdateLevel5State(Level5State.Explain);
    }

    private void Update()
    {
        levelTimer += Time.deltaTime;
    }

    public void UpdateLevel5State(Level5State newState)
    {
        level5State = newState;

        switch (newState)
        {
            case Level5State.Explain:
                break;
            case Level5State.Choose:
                mission_Text.transform.parent.gameObject.SetActive(true);
                mission_Text.text = "將正確的器材放在桌上";
                table.SetActive(true);
                hint.SetActive(true);
                break;
            case Level5State.Place:
                mission_Text.text = "將鋼棉放在培養皿中";
                clip.SetActive(true);
                clip.transform.position = spawnPoint.position;
                steelWool.SetActive(true);
                steelWool.transform.position = spawnPoint2.position;
                vinegar.SetActive(false);
                water.SetActive(false);
                petriDish.SetActive(false);
                steelWool_control.SetActive(true);
                steelWool_test.SetActive(true);
                table.SetActive(false);
                table.GetComponent<Flashing>().StopGlinting();
                if (choose_UI)
                {
                    choose_UI.SetActive(true);
                    Destroy(choose_UI, 7);
                }
                break;
            case Level5State.Water:
                mission_Text.text = "將水加入鋼棉";
                clip.SetActive(false);
                clip.transform.position = spawnPoint.position;
                clip.SetActive(true);
                if (place_UI)
                {
                    place_UI.SetActive(true);
                    Destroy(place_UI, 7);
                }
                break;
            case Level5State.Bag1:
                mission_Text.text = "將鋼棉放進袋子中";
                break;
            case Level5State.Vinegar:
                mission_Text.text = "將醋加入鋼棉";
                break;
            case Level5State.Bag2:
                mission_Text.text = "將鋼棉放進袋子中";
                break;
            case Level5State.Test:
                steelWool_control.SetActive(false);
                steelWool_test.SetActive(false);
                mission_Text.transform.parent.gameObject.SetActive(false);
                break;
        }
    }

    public void UpdateLevel5State_Int(int newState)
    {
        UpdateLevel5State((Level5State)newState);
    }

    public void SendData(string things, bool success = true)
    {
        LearningProcess.data[0] = "單元五";
        LearningProcess.data[1] = things;
        LearningProcess.data[2] = success ? "成功" : "失敗";
        LearningProcess.data[3] = levelTimer.ToString("0");
        learningProcess.DEV_AppendToReport();
    }

    public void SendChooseFailData()
    {
        SendData("拿器材", false);
    }
}