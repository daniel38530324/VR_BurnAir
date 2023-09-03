using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public enum Level4State_PC
{
    Explain,       //說明階段
    Choose,        //選擇器材階段
    Soda,          //放入小蘇達粉階段
    Vinegar,       //加入醋階段
    PlasticBag,    //套住塑膠袋階段
    GlassCover,    //放上透明板
    LimeWater,     //加入澄清石灰水階段
    Shake,         //搖晃塑膠袋階段  
    IncenseSticks, //線香測試階段
    Test           //測驗階段
}

public class Level4Manager_PC : MonoBehaviour
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
    [SerializeField] GameObject soda_UI, vinegar_UI, plasticBag_UI, glassCover_UI, shake_UI, incenseSticks_UI;

    [Header("Object")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject table, soda, vinegar, cover, cover2, glassCover, glassCover2, incenseSticks, incenseSticksTest, plasticBag, plasticBag2, plasticBag_CO2, limeWater, dropper, dropper2;

    public Level4State_PC level4State;
    float levelTimer = 0;

    [Header("Test")]
    [SerializeField] GameObject part2;
    [SerializeField] GameObject questionPanel;
    public QuestionData questionData;
    public Text[] tests;
    public GameObject[] ansPanel;
    int currentQusetIndex;

    bool[] learningState = { true, true, true, true, true, true, true, true };

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
        UpdateLevel4State(Level4State_PC.Explain);
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

    public void UpdateLevel4State(Level4State_PC newState)
    {
        level4State = newState;

        switch (newState)
        {
            case Level4State_PC.Explain:

                break;
            case Level4State_PC.Choose:
                mission_Text.transform.parent.gameObject.SetActive(true);
                mission_Text.text = "將正確的器材放在桌上";
                table.SetActive(true);
                break;
            case Level4State_PC.Soda:
                choose_UI.SetActive(true);
                Destroy(choose_UI, 11);
                mission_Text.text = "加入小蘇打粉";
                part2.GetComponentInChildren<Text>().text = "加入小蘇打粉";
                mouseLook.RemoveThingOnHand();
                table.SetActive(false);
                vinegar.SetActive(false);
                cover.SetActive(false);
                limeWater.SetActive(false);
                soda.transform.position = spawnPoint.position;
                soda.transform.rotation = Quaternion.Euler(0, 0, 0);
                soda.SetActive(true);
                cover2.SetActive(true);
                if (learningState[0])
                {
                    learningState[0] = false;
                    SendData("拿器材");
                }
                break;
            case Level4State_PC.Vinegar:
                //soda_UI.SetActive(true);
                //Destroy(soda_UI, 5);
                mission_Text.text = "加入醋";
                part2.GetComponentInChildren<Text>().text = "加入醋";
                mouseLook.RemoveThingOnHand();
                soda.SetActive(false);
                vinegar.transform.position = spawnPoint.position;
                vinegar.transform.rotation = Quaternion.Euler(0, 0, 0);
                vinegar.SetActive(true);
                if (learningState[1])
                {
                    learningState[1] = false;
                    SendData("加入小蘇達粉");
                }
                break;
            case Level4State_PC.PlasticBag:
                AudioManager.Instance.PlaySound("Level4_2");
                vinegar_UI.SetActive(true);
                Destroy(vinegar_UI, 11);
                mission_Text.text = "套住塑膠袋";
                part2.GetComponentInChildren<Text>().text = "套住塑膠袋";
                mouseLook.RemoveThingOnHand();
                vinegar.SetActive(false);
                dropper.SetActive(false);
                plasticBag.SetActive(true);
                if (learningState[2])
                {
                    learningState[2] = false;
                    SendData("加入醋");
                }
                break;
            case Level4State_PC.GlassCover:
                AudioManager.Instance.PlaySound("Level4_3");
                plasticBag_UI.SetActive(true);
                Destroy(plasticBag_UI, 11);
                mission_Text.text = "放上透明板";
                part2.GetComponentInChildren<Text>().text = "放上透明板";
                mouseLook.RemoveThingOnHand();
                plasticBag.SetActive(false);
                plasticBag2.SetActive(false);
                plasticBag_CO2.SetActive(true);
                glassCover.SetActive(true);
                if (learningState[3])
                {
                    learningState[3] = false;
                    SendData("套住塑膠袋");
                }
                break;
            case Level4State_PC.LimeWater:
                AudioManager.Instance.PlaySound("Level4_4");
                glassCover_UI.SetActive(true);
                Destroy(glassCover_UI, 11);
                mission_Text.text = "將澄清石灰水加到塑膠袋";
                part2.GetComponentInChildren<Text>().text = "將澄清石灰水加到塑膠袋";
                mouseLook.RemoveThingOnHand();
                limeWater.transform.position = spawnPoint.position;
                limeWater.transform.rotation = Quaternion.Euler(0, 0, 0);
                limeWater.SetActive(true);
                if (learningState[4])
                {
                    learningState[4] = false;
                    SendData("放上玻璃蓋");
                }
                break;
            case Level4State_PC.Shake:
                mission_Text.text = "搖晃塑膠袋";
                part2.GetComponentInChildren<Text>().text = "搖晃塑膠袋";
                mouseLook.RemoveThingOnHand();
                limeWater.SetActive(false);
                dropper2.SetActive(false);
                if (learningState[5])
                {
                    learningState[5] = false;
                    SendData("加入澄清石灰水");
                }
                break;
            case Level4State_PC.IncenseSticks:
                AudioManager.Instance.PlaySound("Level4_5");
                shake_UI.SetActive(true);
                Destroy(shake_UI, 11);
                mission_Text.text = "使用線香測試";
                part2.GetComponentInChildren<Text>().text = "使用線香測試";
                mouseLook.RemoveThingOnHand();
                plasticBag_CO2.SetActive(false);
                incenseSticks.SetActive(true);
                incenseSticksTest.SetActive(true);
                if (learningState[6])
                {
                    learningState[6] = false;
                    SendData("搖晃塑膠袋");
                }
                break;
            case Level4State_PC.Test:
                mouseLook.RemoveThingOnHand();
                AudioManager.Instance.PlaySound("Level4_6");
                incenseSticks_UI.SetActive(true);
                Destroy(incenseSticks_UI, 11);
                glassCover2.SetActive(false);
                mission_Text.transform.parent.gameObject.SetActive(false);
                cover2.SetActive(false);
                incenseSticks.SetActive(false);
                part2.SetActive(false);
                questionPanel.SetActive(true);
                Quesion(0);
                if (learningState[7])
                {
                    learningState[7] = false;
                    SendData("使用線香測試");
                }
                break;
        }
    }

    public void UpdateLevel4State_Int(int newState)
    {
        UpdateLevel4State((Level4State_PC)newState);
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
        LearningProcess.data[0] = "單元四";
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
        LearningProcess.data[0] = "單元四";
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
