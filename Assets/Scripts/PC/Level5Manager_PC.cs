using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public enum Level5State_PC
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


public class Level5Manager_PC : MonoBehaviour
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
    [SerializeField] GameObject hint;
    [SerializeField] GameObject choose_UI, place_UI, water_UI, bag1_UI, vinegar_UI, bag2_UI;
    Level5State_PC level5State;

    [Header("Object")]
    [SerializeField] GameObject table;
    [SerializeField] GameObject clip, vinegar, water, dropper, dropper2, petriDish, petriDish2, steelWool, steelWool_test, steelWool_control, bag1, bag2;
    [SerializeField] Transform spawnPoint, spawnPoint2;
    [SerializeField] Text testText, controlText;
    
    [Header("Test")]
    [SerializeField] GameObject part2;
    [SerializeField] GameObject questionPanel;
    public QuestionData questionData;
    public Text[] tests;
    public GameObject[] ansPanel;
    int currentQusetIndex;

    float levelTimer = 0;
    bool[] learningState = { true, true, true, true, true, true };

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        UpdateLevel5State(Level5State_PC.Explain);
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

    public void UpdateLevel5State(Level5State_PC newState)
    {
        if(level5State == newState) return;
        
        level5State = newState;

        switch (newState)
        {
            case Level5State_PC.Explain:
                break;
            case Level5State_PC.Choose:
                mission_Text.transform.parent.gameObject.SetActive(true);
                mission_Text.text = "將正確的器材放在桌上";
                table.SetActive(true);
                hint.SetActive(true);
                break;
            case Level5State_PC.Place:
                AudioManager.Instance.PlaySound("Level5_1");
                mission_Text.text = "將鋼棉放在培養皿中";
                part2.GetComponentInChildren<Text>().text = "將鋼棉放在培養皿中";
                testText.text = "實驗組(空氣)";
                controlText.text = "對照組(空氣)";
                mouseLook.RemoveThingOnHand();
                clip.SetActive(true);
                clip.transform.position = spawnPoint.position;
                clip.transform.rotation = Quaternion.Euler(0, 90, 0);
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
                    Destroy(choose_UI, 11);
                }
                if (learningState[0])
                {
                    learningState[0] = false;
                    SendData("拿器材");
                }
                break;
            case Level5State_PC.Water:
                AudioManager.Instance.PlaySound("Level5_2");
                steelWool_control.transform.GetChild(0).gameObject.SetActive(false);
                steelWool_control.transform.GetChild(1).gameObject.SetActive(true);
                mouseLook.RemoveThingOnHand();
                clip.SetActive(false);
                mission_Text.text = "將水加入鋼棉";
                part2.GetComponentInChildren<Text>().text = "將水加入鋼棉";
                testText.text = "實驗組(加入水)";
                controlText.text = "對照組(空氣)";
                water.transform.position = spawnPoint.position;
                water.transform.rotation = Quaternion.identity;
                water.SetActive(true);
                if (place_UI)
                {
                    place_UI.SetActive(true);
                    Destroy(place_UI, 11);
                }
                if (learningState[1])
                {
                    learningState[1] = false;
                    SendData("將鋼棉放在培養皿中");
                }
                break;
            case Level5State_PC.Bag1:
                steelWool_test.transform.GetChild(0).gameObject.SetActive(false);
                mouseLook.RemoveThingOnHand();
                water.SetActive(false);
                dropper.SetActive(false);
                bag1.SetActive(true);
                clip.SetActive(true);
                clip.transform.position = spawnPoint.position;
                clip.transform.rotation = Quaternion.Euler(0, 90, 0);
                petriDish2.SetActive(true);
                steelWool.SetActive(true);
                steelWool.transform.position = spawnPoint2.position;
                mission_Text.text = "將鋼棉放進袋子中";
                part2.GetComponentInChildren<Text>().text = "將鋼棉放進袋子中";
                /*
                if (water_UI)
                {
                    water_UI.SetActive(true);
                    Destroy(water_UI, 7);
                }
                */
                if (learningState[2])
                {
                    learningState[2] = false;
                    SendData("將水加入鋼棉");
                }
                break;
            case Level5State_PC.Vinegar:
                mouseLook.RemoveThingOnHand();
                petriDish2.SetActive(false);
                steelWool_test.transform.GetChild(0).gameObject.SetActive(true);
                bag1.SetActive(false);
                clip.SetActive(false);
                vinegar.transform.position = spawnPoint.position;
                vinegar.transform.rotation = Quaternion.identity;
                vinegar.SetActive(true);
                mission_Text.text = "將醋加入鋼棉";
                part2.GetComponentInChildren<Text>().text = "將醋加入鋼棉";
                testText.text = "實驗組(加入醋)";
                controlText.text = "對照組(加入水)";
                if (bag1_UI)
                {
                    bag1_UI.SetActive(true);
                    Destroy(bag1_UI, 7);
                }
                if (learningState[3])
                {
                    learningState[3] = false;
                    SendData("將鋼棉放進袋子中");
                }
                break;
            case Level5State_PC.Bag2:
                mouseLook.RemoveThingOnHand();
                steelWool_test.transform.GetChild(0).gameObject.SetActive(false);
                vinegar.SetActive(false);
                dropper2.SetActive(false);
                bag2.SetActive(true);
                clip.SetActive(true);
                clip.transform.position = spawnPoint.position;
                clip.transform.rotation = Quaternion.Euler(0, 90, 0);
                petriDish2.SetActive(true);
                steelWool.SetActive(true);
                steelWool.transform.position = spawnPoint2.position;
                mission_Text.text = "將鋼棉放進袋子中";
                /*
                if (vinegar_UI)
                {
                    vinegar_UI.SetActive(true);
                    Destroy(vinegar_UI, 7);
                }
                */
                if (learningState[4])
                {
                    learningState[4] = false;
                    SendData("將醋加入鋼棉");
                }
                break;
            case Level5State_PC.Test:
                if (bag2_UI)
                {
                    bag2_UI.SetActive(true);
                    Destroy(bag2_UI, 11);
                }
                AudioManager.Instance.PlaySound("Level5_6");
                mouseLook.RemoveThingOnHand();
                petriDish2.SetActive(false);
                clip.SetActive(false);
                steelWool_control.SetActive(false);
                steelWool_test.SetActive(false);
                bag1.SetActive(false);
                bag2.SetActive(false);
                mission_Text.transform.parent.gameObject.SetActive(false);
                part2.SetActive(false);
                questionPanel.SetActive(true);
                Quesion(0);
                if (learningState[5])
                {
                    learningState[5] = false;
                    SendData("將鋼棉放進袋子中");
                }
                break;
        }
    }

    public void ReturnLevelState(Level5State_PC newState)
    {
        switch (newState)
        {
            case Level5State_PC.Water:
                water.SetActive(false);
                water.transform.position = spawnPoint.position;
                water.transform.rotation = Quaternion.Euler(0, 90, 0);
                water.SetActive(true);
                break;
            case Level5State_PC.Vinegar:
                vinegar.SetActive(false);
                vinegar.transform.position = spawnPoint.position;
                vinegar.transform.rotation = Quaternion.Euler(0, 90, 0);
                vinegar.SetActive(true);
                break;
        }
    }

    public void UpdateLevel5State_Int(int newState)
    {
        UpdateLevel5State((Level5State_PC)newState);
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
        LearningProcess.data[0] = "單元五";
        LearningProcess.data[1] = questionData.questions[currentQusetIndex];
        LearningProcess.data[2] = correctAns ? "答對" : "答錯";
        LearningProcess.data[3] = levelTimer.ToString("0");
        learningProcess.DEV_AppendToReport();
        
        if(correctAns)
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