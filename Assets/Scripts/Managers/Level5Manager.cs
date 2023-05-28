using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public enum Level5State
{
    Explain,            //�������q
    Choose,             //��ܾ������q
    Place,              //�Ů�-��m���ֶ��q
    Bag1,               //��-��i�U�l���q
    Water,              //��-�[�J�����q
    Bag2,               //�L-��i�U�l���q
    vinegar,            //�L-�[�J�L���q
    Test,               //���綥�q
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