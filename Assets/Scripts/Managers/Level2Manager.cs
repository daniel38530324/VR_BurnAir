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

    [Header("LearningProcess")]
    [SerializeField] LearningProcess learningProcess;

    [Header("Mission")]
    [SerializeField] Text mission_Text;
    [SerializeField] Text mission_Text2;
    [SerializeField] Text timer_Text;
    public GameObject Combustible_UI, AlcoholLamp_UI, WaterBucket_UI, FireEx_DryPowder_UI, FireEx_Metal_UI;

    [Header("Object")]
    [SerializeField] GameObject fires;
    [SerializeField] GameObject combustibles, extinguishingTools;
    [SerializeField] GameObject bucket, bucketSpawnPoint;
    [SerializeField] GameObject wrong_UI;

    [Header("Panel")]
    [SerializeField] GameObject part2Panel, title;
    [SerializeField] GameObject successPanel;
    [SerializeField] GameObject defeatPanel;
    [SerializeField] GameObject questionPanel;

    [SerializeField] Transform[] equipmentPoints;

    [SerializeField] GameObject[] finishs;

    [Header("Test")]
    public QuestionData questionData;
    public Text[] tests;
    public GameObject[] ansPanel;
    int currentQusetIndex;
    
    public int combustiblesCount = 0, fireCount = 0;
    public Level2State level2State;
    float timer = 90;
    bool timerState;
    float levelTimer = 0;
    bool[] learningState = {true, true};

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        UpdateLevel2State(Level2State.Explain);
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic("Scene");
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
                part2Panel.SetActive(false);
                defeatPanel.SetActive(true);
            }
        }

        levelTimer += Time.deltaTime;
    }

    public void UpdateLevel2State(Level2State newState)
    {
        level2State = newState;

        switch(newState)
        {
            case Level2State.Explain:
                break;
            case Level2State.Combustibles:
                timer = 180;
                combustiblesCount = 0;
                mission_Text.transform.parent.gameObject.SetActive(true);
                mission_Text2.gameObject.SetActive(true);
                UpdateCombustiblesCount();
                combustibles.SetActive(true);
                fires.SetActive(true);
                timerState = true;
                break;
            case Level2State.Fire:
                GetKnowledgePoints(Combustible_UI, false);
                mission_Text2.gameObject.SetActive(false);
                timer = 270;
                fireCount = 0;
                UpdateFireCount();
                extinguishingTools.SetActive(true);
                if(learningState[0])
                {
                    learningState[0] = false;
                    SendData("移除可燃物");
                }
                
                break;
            case Level2State.Success:
                timerState = false;
                timer_Text.transform.parent.gameObject.SetActive(false);
                mission_Text.text = "前往黑板進行測驗";
                part2Panel.SetActive(false);
                successPanel.SetActive(true);
                if(learningState[1])
                {
                    learningState[1] = false;
                    SendData("滅掉火源");
                }
                break;
            case Level2State.Test:
                title.SetActive(false);
                extinguishingTools.SetActive(false);
                successPanel.SetActive(false);
                defeatPanel.SetActive(false);
                questionPanel.SetActive(true);
                Quesion(0);
                break;
        }
    }

    public void ReturnLevelState(Level2State newState)
    {
        switch(newState)
        {
            case Level2State.Fire:
                bucket.SetActive(false);
                bucket.transform.position = bucketSpawnPoint.transform.position;
                bucket.transform.rotation = Quaternion.identity;
                bucket.SetActive(true);
                break;
        }
    }

    public void UpdateLevel2State_Int(int newState)
    {
        UpdateLevel2State((Level2State)newState);
    }

    public void UpdateCombustiblesCount()
    {
        mission_Text.text = "移除可燃物:" + combustiblesCount + "/4";
        part2Panel.GetComponentInChildren<Text>().text = "移除可燃物";
        if (combustiblesCount >= 4)
        {
            UpdateLevel2State(Level2State.Fire);
        }
    }

    public void UpdateFireCount()
    {
        mission_Text.text = "滅掉火源:" + fireCount + "/4";
        part2Panel.GetComponentInChildren<Text>().text = "滅掉火源";
        if (fireCount >= 4)
        {
            UpdateLevel2State(Level2State.Success);
        }
    }

    public void GetKnowledgePoints(GameObject knowledgePoint, bool wait)
    {
        StartCoroutine(KnowledgePoints(knowledgePoint, wait));
    }

    IEnumerator KnowledgePoints(GameObject knowledgePoint, bool wait)
    {
        if(wait)
        {
            yield return new WaitForSeconds(5);
        }
        knowledgePoint.SetActive(true);
        timerState = false;
        yield return new WaitForSeconds(18);
        knowledgePoint.SetActive(false);
        if(level2State != Level2State.Success && level2State != Level2State.CombustiblesFail && level2State != Level2State.FireFail)
        {
            timerState = true;
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
        LearningProcess.data[0] = "單元二";
        LearningProcess.data[1] = questionData.questions[currentQusetIndex];
        LearningProcess.data[2] = correctAns ? "答對" : "答錯";
        LearningProcess.data[3] = levelTimer.ToString("0");
        learningProcess.DEV_AppendToReport();

        if (correctAns)
        {
            AudioManager.Instance.PlaySound("Correct");
        }
        else
        {
            AudioManager.Instance.PlaySound("Fail");
        }

        tests[0].text = questionData.explain[currentQusetIndex];
        currentQusetIndex++;
        ansPanel[0].SetActive(correctAns);
        ansPanel[1].SetActive(!correctAns);
        yield return new WaitForSeconds(5f);
        if(questionData.questions.Length == currentQusetIndex){
            GameManager.levelState[1] = true;
            SceneManager.LoadScene("MainPage");
        }else{
            Quesion(currentQusetIndex);
            ansPanel[0].SetActive(false);
            ansPanel[1].SetActive(false);
        }
    }

    public void SendData(string things, bool success = true)
    {
        LearningProcess.data[0] = "單元二";
        LearningProcess.data[1] = things;
        LearningProcess.data[2] = success ? "成功" : "失敗";
        LearningProcess.data[3] = levelTimer.ToString("0");
        learningProcess.DEV_AppendToReport();
    }

    public void SendChooseFailData()
    {
        SendData("拿器材", false);
    }

    public void RetuenPosition(Transform equipment)
    {
        foreach (Transform item in equipmentPoints)
        {
            if (equipment.name == item.name)
            {
                equipment.GetComponent<Rigidbody>().isKinematic = true;
                equipment.position = item.position;
                equipment.rotation = item.rotation;
                equipment.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    public void CheckFinish(int index)
    {
        finishs[index].SetActive(true);
    }

    public void GetWrong()
    {
        StartCoroutine(Wrong());
    }

    IEnumerator Wrong()
    {
        timerState = false;
        wrong_UI.SetActive(true);
        AudioManager.Instance.PlaySound("FireWrong");
        yield return new WaitForSeconds(5);
        wrong_UI.SetActive(false);
        timerState = true;
    }
}