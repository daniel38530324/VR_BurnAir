using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Level1State
{
    Explain, //說明階段
    Choose,  //選擇器材階段
    Fan,     //搧扇子階段
    Cover,   //蓋住火焰階段
    Bucket,  //水桶階段
    Flour,   //麵粉階段
    Test     //測驗階段
}

public class Level1Manager : MonoBehaviour
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

    [Header("Object")]
    [SerializeField] GameObject table;
    [SerializeField] GameObject fan, cover, bucket, flour, candle_control, candle_test;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Candle candle;

    public Level1State level1State;
    
    [Header("Test")]
    public QuestionData questionData;
    public GameObject part2Panel;
    public GameObject questionPanel;
    public Text[] tests;
    public GameObject[] ansPanel;
    int currentQusetIndex;

    float levelTimer = 0;
    bool[] learningState = { true, true, true, true, true };

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        UpdateLevel1State(Level1State.Explain);
    }

    private void Update()
    {
        levelTimer += Time.deltaTime;
    }

    public void UpdateLevel1State(Level1State newState)
    {
        level1State = newState;

        switch (newState)
        {
            case Level1State.Explain:
                break;
            case Level1State.Choose:
                mission_Text.transform.parent.gameObject.SetActive(true);
                mission_Text.text = "將正確的器材放在桌上";
                table.SetActive(true);
                //table.GetComponent<Flashing>().StartGlinting();
                /*
                for (int i = 0; i < 6; i++)
                {
                    table.transform.GetChild(i).GetComponent<Flashing>().StartGlinting();
                }
                */
                hint.SetActive(true);
                break;
            case Level1State.Fan:
                fan.SetActive(true);
                fan.transform.position = spawnPoint.position;
                fan.transform.rotation = Quaternion.Euler(0, 90, 0);
                cover.SetActive(false);
                bucket.SetActive(false);
                flour.SetActive(false);
                mission_Text.text = "用扇子慢慢搧火焰";
                candle_control.SetActive(true);
                candle_test.SetActive(true);
                table.SetActive(false);
                table.GetComponent<Flashing>().StopGlinting();
                /*
                for (int i = 0; i < 6; i++)
                {
                    table.transform.GetChild(i).GetComponent<Flashing>().StopGlinting();
                }
                */
                if(choose_UI)
                {
                    choose_UI.SetActive(true);
                    Destroy(choose_UI, 7);
                }
                if(learningState[0])
                {
                    learningState[0] = false;
                    SendData("拿器材");
                }
                break;
            case Level1State.Cover:
                candle.ReturnFire();
                fan.SetActive(false);
                cover.SetActive(true);
                cover.transform.position = spawnPoint.position;
                cover.transform.localRotation = Quaternion.Euler(0, 90, 0);
                mission_Text.text = "將火焰蓋住";
                if(fan_UI)
                {
                    fan_UI.SetActive(true);
                    Destroy(fan_UI, 7);
                }
                if(learningState[1])
                {
                    learningState[1] = false;
                    SendData("扇子搧火");
                }
                break;
            case Level1State.Bucket:
                candle.ReturnFire();
                cover.SetActive(false);
                bucket.transform.position = spawnPoint.position;
                bucket.transform.rotation = Quaternion.identity;
                bucket.SetActive(true);
                bucket.GetComponent<WaterBucket_New>().enabled = true;
                mission_Text.text = "將火焰澆熄";
                if(cover_UI)
                {
                    cover_UI.SetActive(true);
                    Destroy(cover_UI, 7);
                }
                if(learningState[2])
                {
                    learningState[2] = false;
                    SendData("蓋子滅火");
                }
                break;
            case Level1State.Flour:
                candle.ReturnFire();
                bucket.SetActive(false);           
                flour.transform.position = spawnPoint.position;
                flour.transform.rotation = Quaternion.identity;
                flour.SetActive(true);
                flour.GetComponent<WaterBucket_New>().enabled = true;
                mission_Text.text = "將麵粉加入火中";
                if(bucket_UI)
                {
                    bucket_UI.SetActive(true);
                    Destroy(bucket_UI, 7);
                }
                if(learningState[3])
                {
                    learningState[3] = false;
                    SendData("用水滅火");
                }
                break;
            case Level1State.Test:
                flour.SetActive(false);
                candle_control.SetActive(false);
                candle_test.SetActive(false);
                mission_Text.transform.parent.gameObject.SetActive(false);
                part2Panel.SetActive(false);
                questionPanel.SetActive(true);
                Quesion(0);
                flour_UI.SetActive(true);
                Destroy(flour_UI, 7);
                if(learningState[4])
                {
                    learningState[4] = false;
                    SendData("麵粉加入火");
                }
                break;
        }
    }

    public void ReturnLevelState(Level1State newState)
    {
        switch (newState)
        {
            case Level1State.Fan:
                fan.SetActive(false);
                fan.transform.position = spawnPoint.position;
                fan.transform.rotation = Quaternion.Euler(0, 90, 0);
                fan.SetActive(true);
                break;
            case Level1State.Cover:
                cover.SetActive(false);
                cover.transform.position = spawnPoint.position;
                cover.transform.localRotation = Quaternion.Euler(0, 90, 0);
                cover.SetActive(true);
                break;
            case Level1State.Bucket:
                bucket.SetActive(false);
                bucket.transform.position = spawnPoint.position;
                bucket.transform.rotation = Quaternion.identity;
                bucket.SetActive(true);
                break;
            case Level1State.Flour:
                flour.SetActive(false);
                flour.transform.position = spawnPoint.position;
                flour.transform.rotation = Quaternion.identity;
                flour.SetActive(true);
                break;
        }
    }

    public void UpdateLevel1State_Int(int newState)
    {
        UpdateLevel1State((Level1State)newState);
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
        LearningProcess.data[0] = "單元一";
        LearningProcess.data[1] = questionData.questions[currentQusetIndex];
        LearningProcess.data[2] = correctAns ? "答對" : "答錯";
        LearningProcess.data[3] = levelTimer.ToString("0");
        learningProcess.DEV_AppendToReport();

        tests[0].text = questionData.explain[currentQusetIndex];
        currentQusetIndex++;
        ansPanel[0].SetActive(correctAns);
        ansPanel[1].SetActive(!correctAns);
        yield return new WaitForSeconds(2f);
        if(questionData.questions.Length == currentQusetIndex){
            SceneManager.LoadScene("MainPage");
        }else{
            Quesion(currentQusetIndex);
            ansPanel[0].SetActive(false);
            ansPanel[1].SetActive(false);
        }
    }

    public void SendData(string things, bool success = true)
    {
        LearningProcess.data[0] = "單元一";
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
