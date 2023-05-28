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
    Bag1,               //水-放進袋子階段
    Water,              //水-加入水階段
    Bag2,               //醋-放進袋子階段
    vinegar,            //醋-加入醋階段
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
    [SerializeField] GameObject choose_UI, fan_UI, cover_UI, bucket_UI, flour_UI;
    Level5State level5State;

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

        }
    }
}