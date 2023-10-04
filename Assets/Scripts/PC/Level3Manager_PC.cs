using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Level3State_PC
{
    Explain,       //說明階段
    Choose,        //選擇器材階段
    Mushroom,      //放入金針菇階段
    H2O2,          //加入雙氧水階段
    GlassCover,    //放上透明板
    IncenseSticks, //線香測試階段
    Test           //測驗階段
}

public class Level3Manager_PC : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameObject gameManager;
    [SerializeField] MouseLook mouseLook;

    [Header("LearningProcess")]
    [SerializeField] LearningProcess learningProcess;

    [Header("Mission")]
    [SerializeField] Text mission_Text;

    [Header("Knowledge points")]
    [SerializeField] GameObject hint_image;
    [SerializeField] GameObject hint_panel;
    [SerializeField] GameObject choose_UI;
    [SerializeField] GameObject mushroom_UI, h2O2_UI, glassCover_UI, incenseSticks_UI;

    [Header("Object")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject table, mushroom, h2O2, cover, cover2, glassCover, glassCover2, incenseSticks, incenseSticksTest;

    public Level3State_PC level3State;
    float levelTimer = 0;

    [Header("Test")]
    [SerializeField] GameObject part2;
    [SerializeField] GameObject questionPanel;
    public QuestionData questionData;
    public Text[] tests;
    public GameObject[] ansPanel;
    int currentQusetIndex;

    bool[] learningState = { true, true, true, true, true};

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
        UpdateLevel3State(Level3State_PC.Explain);
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic("Scene");
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)){
            hint_image.SetActive(!hint_image.activeSelf);
            hint_panel.SetActive(!hint_panel.activeSelf);
        }
        
        levelTimer += Time.deltaTime;
    }

    public void UpdateLevel3State(Level3State_PC newState)
    {
        if(level3State == newState) return;
        
        level3State = newState;

        switch (newState)
        {
            case Level3State_PC.Explain:

                break;
            case Level3State_PC.Choose:
                mission_Text.transform.parent.gameObject.SetActive(true);
                mission_Text.text = "將正確的器材放在桌上";
                table.SetActive(true);
                break;
            case Level3State_PC.Mushroom:
             AudioManager.Instance.PlaySound("Level3_1");
                choose_UI.SetActive(true);
                Destroy(choose_UI, 11);
                mission_Text.text = "加入金針菇";
                part2.GetComponentInChildren<Text>().text = "將金針菇倒入廣口瓶中";
                mouseLook.RemoveThingOnHand();
                table.SetActive(false);
                h2O2.SetActive(false);
                cover.SetActive(false);
                mushroom.transform.position = spawnPoint.position;
                mushroom.transform.rotation = Quaternion.Euler(0, 0, 0);
                mushroom.SetActive(true);
                cover2.SetActive(true);
                if (learningState[0])
                {
                    learningState[0] = false;
                    SendData("拿器材");
                }
                break;
            case Level3State_PC.H2O2:
                //mushroom_UI.SetActive(true);
                //Destroy(mushroom_UI, 5);
                mission_Text.text = "加入雙氧水";
                 part2.GetComponentInChildren<Text>().text = "將雙氧水倒入廣口瓶中";
                mouseLook.RemoveThingOnHand();
                mushroom.SetActive(false);
                h2O2.transform.position = spawnPoint.position;
                h2O2.transform.rotation = Quaternion.Euler(0, 0, 0);
                h2O2.SetActive(true);
                if (learningState[1])
                {
                    learningState[1] = false;
                    SendData("加入金針菇");
                }
                break;
            case Level3State_PC.GlassCover:
                AudioManager.Instance.PlaySound("Level3_2");
                h2O2_UI.SetActive(true);
                Destroy(h2O2_UI, 11);
                mission_Text.text = "放上透明板";
                part2.GetComponentInChildren<Text>().text = "將透明板蓋住廣口瓶";
                mouseLook.RemoveThingOnHand();
                h2O2.SetActive(false);
                glassCover.SetActive(true);
                if (learningState[2])
                {
                    learningState[2] = false;
                    SendData("加入雙氧水");
                }
                break;
            case Level3State_PC.IncenseSticks:
                AudioManager.Instance.PlaySound("Level3_3");
                glassCover_UI.SetActive(true);
                Destroy(glassCover_UI, 11);
                mission_Text.text = "移開透明板並用線香進行測試";
                part2.GetComponentInChildren<Text>().text = "稍微移開透明板並用線香放入廣口瓶中進行測試";
                mouseLook.RemoveThingOnHand();
                incenseSticks.SetActive(true);
                incenseSticksTest.SetActive(true);
                if (learningState[3])
                {
                    learningState[3] = false;
                    SendData("放上玻璃蓋");
                }
                break;
            case Level3State_PC.Test:
                AudioManager.Instance.PlaySound("Level3_4");
                incenseSticks_UI.SetActive(true);
                mouseLook.RemoveThingOnHand();
                Destroy(incenseSticks_UI, 11);
                glassCover2.SetActive(false);
                mission_Text.transform.parent.gameObject.SetActive(false);
                cover2.SetActive(false);
                incenseSticks.SetActive(false);
                part2.SetActive(false);
                questionPanel.SetActive(true);
                Quesion(0);
                if (learningState[4])
                {
                    learningState[4] = false;
                    SendData("使用線香測試");
                }
                break;
        }
    }

    public void UpdateLevel3State_Int(int newState)
    {
        UpdateLevel3State((Level3State_PC)newState);
    }

    void Quesion(int index)
    {
        tests[0].text = questionData.questions[currentQusetIndex];
        tests[1].text = questionData.answer1[currentQusetIndex];
        tests[2].text = questionData.answer2[currentQusetIndex];
    }

    public void AnsBtn(bool isRight)
    {
        if (questionData.correctAnswerIsRight[currentQusetIndex])
        {
            if (isRight)
            {
                StartCoroutine(NextQusetion(true));
            }
            else
            {
                StartCoroutine(NextQusetion(false));
            }
        }
        else
        {
            if (!isRight)
            {
                StartCoroutine(NextQusetion(true));
            }
            else
            {
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
        yield return new WaitForSeconds(2f);
        if (questionData.questions.Length == currentQusetIndex)
        {
            Cursor.lockState = CursorLockMode.Confined;
            SceneManager.LoadScene("MainPage_PC");
        }
        else
        {
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
