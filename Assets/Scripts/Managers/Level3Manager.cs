using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.SceneManagement;

public enum Level3State
{
    Explain,       //說明階段
    Choose,        //選擇器材階段
    MnO2,          //放入二氧化錳階段
    Water,         //加入水階段
    Tube,          //管子階段
    Cover,         //放入杯子階段
    H2O2,          //加入雙氧水階段
    GlassCover,    //放入玻璃片
    PickUp,        //拿起階段
    IncenseSticks, //線香測試階段
    Test           //測驗階段
}
public class Level3Manager : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameObject gameManager;

    [Header("LearningProcess")]
    [SerializeField] LearningProcess learningProcess;

    [Header("Mission")]
    [SerializeField] Text mission_Text;

    [Header("Knowledge points")]
    [SerializeField] GameObject choose_UI;
    [SerializeField] GameObject h2O2_UI, glassCover_UI,cover_UI, pickUp_UI, incenseSticks_UI;

    [Header("Object")]
    [SerializeField] GameObject waterTank;
    [SerializeField] GameObject suctionBottle, table, waterBucket,cover, mnO2, mnO2withWater, h2O2, dropper, pipe, bottle, incenseSticks, bottleForIncenseSticks, glassCover, glassCoverInWater, bubbleEffect;

    [SerializeField] Transform spawnPoint;

    public Level3State level3State;

    [Header("Test")]
    [SerializeField] GameObject part2;
    [SerializeField] GameObject questionPanel;
    public QuestionData questionData;
    public Text[] tests;
    public GameObject[] ansPanel;
    int currentQusetIndex;

    float levelTimer = 0;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        UpdateLevel3State(Level3State.Explain);
    }

    private void Update()
    {
        levelTimer += Time.deltaTime;
    }

    public void UpdateLevel3State(Level3State newState)
    {
        level3State = newState;

        switch (newState)
        {
            case Level3State.Explain:
                
                break;
            case Level3State.Choose:
                mission_Text.transform.parent.gameObject.SetActive(true);
                mission_Text.text = "將正確的器材放在桌上";
                table.SetActive(true);
                break;
            case Level3State.MnO2:
                choose_UI.SetActive(true);
                Destroy(choose_UI, 5);
                mission_Text.text = "加入二氧化錳";
                table.SetActive(false);
                waterBucket.SetActive(false);
                h2O2.SetActive(false);
                cover.SetActive(false);
                mnO2.transform.position = spawnPoint.position;
                mnO2.transform.rotation = Quaternion.Euler(0, 0, 0);
                mnO2.SetActive(true);
                mnO2.GetComponent<WaterBucket_New>().enabled = true;
                waterTank.SetActive(true);
                suctionBottle.SetActive(true);
                SendData("拿器材");
                break;
            case Level3State.Water:
                mission_Text.text = "加入水";
                mnO2.SetActive(false);
                waterBucket.transform.position = spawnPoint.position;
                waterBucket.transform.rotation = Quaternion.Euler(0, -90, 0);
                mnO2withWater.SetActive(true);
                waterBucket.SetActive(true);
                waterBucket.GetComponent<WaterBucket_New>().enabled = true;
                SendData("加入二氧化錳");
                break;
            case Level3State.Tube:
                mission_Text.text = "放入管子";
                waterBucket.SetActive(false);
                pipe.SetActive(true);
                SendData("加入水");
                break;
            case Level3State.Cover:
                mission_Text.text = "放入杯子";
                bottle.SetActive(true);
                SendData("放入管子");
                break;
            case Level3State.H2O2:
                cover_UI.SetActive(true);
                Destroy(cover_UI, 5);
                mission_Text.text = "加入雙氧水";
                h2O2.transform.position = spawnPoint.position;
                h2O2.transform.rotation = Quaternion.Euler(0, -180, 0);
                h2O2.SetActive(true);
                h2O2.GetComponent<WaterBucket_New>().enabled = true;
                //h2O2.GetComponent<XRGrabInteractable>().enabled = false;
                //dropper.GetComponent<XRGrabInteractable>().enabled = true;
                SendData("放入杯子");
                break;
            case Level3State.GlassCover:
                h2O2_UI.SetActive(true);
                Destroy(h2O2_UI, 5);
                glassCover.SetActive(true);
                mission_Text.text = "放入玻璃蓋";
                h2O2.SetActive(false);
                //dropper.SetActive(false);
                SendData("加入雙氧水");
                break;
            case Level3State.PickUp:
                glassCover_UI.SetActive(true);
                Destroy(glassCover_UI, 5);
                glassCover.SetActive(false);
                bubbleEffect.SetActive(false);
                glassCoverInWater.SetActive(true);
                mission_Text.text = "拿起杯子";
                SendData("放入玻璃蓋");
                break;
            case Level3State.IncenseSticks:
                pickUp_UI.SetActive(true);
                Destroy(pickUp_UI, 5);
                incenseSticks.SetActive(true);
                bottleForIncenseSticks.SetActive(true);
                mission_Text.text = "拿下戴玻片並用線香測試";
                SendData("拿起杯子");
                break;
            case Level3State.Test:
                incenseSticks_UI.SetActive(true);
                Destroy(incenseSticks_UI, 5);
                incenseSticks.SetActive(false);
                bottleForIncenseSticks.SetActive(false);
                waterTank.SetActive(false);
                suctionBottle.SetActive(false);
                glassCoverInWater.SetActive(false);
                mission_Text.transform.parent.gameObject.SetActive(false);
                part2.SetActive(false);
                questionPanel.SetActive(true);
                Quesion(0);
                SendData("拿下戴玻片並用線香測試");
                break;
        }
    }
    
    public void ReturnLevelState(Level3State newState)
    {
        switch(newState)
        {
            case Level3State.MnO2:
                mnO2.SetActive(false);
                mnO2.transform.position = spawnPoint.position;
                mnO2.transform.rotation = Quaternion.Euler(0, 0, 0);
                mnO2.SetActive(true);
                break;
            case Level3State.Water:
                waterBucket.SetActive(false);
                waterBucket.transform.position = spawnPoint.position;
                waterBucket.transform.rotation = Quaternion.Euler(0, 0, 0);
                waterBucket.SetActive(true);
                break;
            case Level3State.H2O2:
                h2O2.SetActive(false);
                h2O2.transform.position = spawnPoint.position;
                h2O2.transform.rotation = Quaternion.Euler(0, 0, 0);
                h2O2.SetActive(true);
                break;
        }
    }

    public void UpdateLevel3State_Int(int newState)
    {
        UpdateLevel3State((Level3State)newState);
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
        LearningProcess.data[0] = "單元三";
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
            SceneManager.LoadScene("Level4");
        }else{
            Quesion(currentQusetIndex);
            ansPanel[0].SetActive(false);
            ansPanel[1].SetActive(false);
        }
    }

    public void SendData(string things, bool success = true)
    {
        LearningProcess.data[0] = "單元三";
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
