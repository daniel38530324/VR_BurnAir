using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public enum Level2State
{
    Explain,            //說明階段
    Combustibles,       //易燃物階段
    Fire,               //滅火階段
    Success,            //成功階段
    CombustiblesFail,   //失敗階段
    FireFail,           //失敗階段
    Test,               //測驗階段
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

    [Header("Panel")]
    [SerializeField] GameObject successPanel;
    [SerializeField] GameObject defeatPanel;
    [SerializeField] GameObject questionPanel;
    
    [Header("Test")]
    public QuestionData questionData;
    public Text[] tests;
    public GameObject[] ansPanel;
    int currentQusetIndex;
    
    public int combustiblesCount = 0, fireCount = 0;
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
        if(Input.GetKeyDown(KeyCode.V)){
            if(level2State == Level2State.Fire){
                fireCount = 4;
                UpdateFireCount();
            }else if(level2State == Level2State.Combustibles){
                combustiblesCount = 4;
                UpdateCombustiblesCount();
            }
        }
        if(Input.GetKeyDown(KeyCode.D)){
            timer = 5;
        }
        
        if(timerState)
        {
            timer -= Time.deltaTime;
            timer_Text.text = timer.ToString("0");

            if (timer <= 0){
                timerState = false;
                mission_Text.transform.parent.gameObject.SetActive(false);
                defeatPanel.SetActive(true);
            }
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
                timer = 90;
                combustiblesCount = 0;
                mission_Text.transform.parent.gameObject.SetActive(true);
                UpdateCombustiblesCount();
                combustibles.SetActive(true);
                fires.SetActive(true);
                timerState = true;
                break;
            case Level2State.Fire:
                timer = 120;
                fireCount = 0;
                UpdateFireCount();
                extinguishingTools.SetActive(true);
                break;
            case Level2State.Success:
                timerState = false;
                mission_Text.transform.parent.gameObject.SetActive(false);
                successPanel.SetActive(true);
                extinguishingTools.SetActive(false);
                break;
            case Level2State.Test:
                questionPanel.SetActive(true);
                Quesion(0);
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

    public void ResetBtn()
    {
        // if(level2State == Level2State.Combustibles){
        //     UpdateLevel2State(Level2State.Combustibles);
        // }else if(level2State == Level2State.Fire){
        //     UpdateLevel2State(Level2State.Fire);
        // }
        SceneManager.LoadScene("Level2");
    }

    public void TestBtn()
    {
        UpdateLevel2State(Level2State.Test);
    }

    void Quesion(int index)
    {
        tests[0].text = questionData.questions[currentQusetIndex];
        tests[1].text = questionData.answer1[currentQusetIndex];
        tests[2].text = questionData.answer2[currentQusetIndex];
    }

    public void AnsBtn(bool isRight)
    {
        if(questionData.correctAnswerIsRight[currentQusetIndex]){
            if(isRight){
                StartCoroutine(NextQusetion(true));
            }else{
                StartCoroutine(NextQusetion(false));
            }
        }else{
            if(!isRight){
                StartCoroutine(NextQusetion(true));
            }else{
                StartCoroutine(NextQusetion(false));
            }
        }
    }

    IEnumerator NextQusetion(bool correctAns)
    {
        tests[0].text = questionData.explain[currentQusetIndex];
        currentQusetIndex++;
        ansPanel[0].SetActive(correctAns);
        ansPanel[1].SetActive(!correctAns);
        yield return new WaitForSeconds(2f);
        if(questionData.questions.Length == currentQusetIndex){
            //SceneManager.LoadScene("Level3");
        }else{
            Quesion(currentQusetIndex);
            ansPanel[0].SetActive(false);
            ansPanel[1].SetActive(false);
        }
    }
}