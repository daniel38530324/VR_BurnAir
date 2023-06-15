using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.XR.Interaction.Toolkit;
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
    [SerializeField] GameObject clip, vinegar, water, dropper, dropper2, petriDish, steelWool, steelWool1, steelWool2, steelWool_test, steelWool_control, bag1, bag2;
    [SerializeField] Transform spawnPoint, spawnPoint2;
    
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
                    Destroy(choose_UI, 7);
                }
                break;
            case Level5State.Water:
                clip.SetActive(false);
                mission_Text.text = "將水加入鋼棉";
                water.transform.position = spawnPoint.position;
                water.transform.rotation = Quaternion.identity;
                water.SetActive(true);
                water.GetComponent<XRGrabInteractable>().enabled = false;
                dropper.GetComponent<XRGrabInteractable>().enabled = true;
                if (place_UI)
                {
                    place_UI.SetActive(true);
                    Destroy(place_UI, 7);
                }
                break;
            case Level5State.Bag1:
                water.SetActive(false);
                dropper.SetActive(false);
                bag1.SetActive(true);
                clip.SetActive(true);
                clip.transform.position = spawnPoint.position;
                clip.transform.rotation = Quaternion.Euler(0, 90, 0);
                steelWool1.transform.SetParent(null);
                steelWool1.tag = "SteelWool";
                steelWool1.GetComponent<Rigidbody>().isKinematic = false;
                mission_Text.text = "將鋼棉放進袋子中";
                if (water_UI)
                {
                    water_UI.SetActive(true);
                    Destroy(water_UI, 7);
                }
                break;
            case Level5State.Vinegar:
                clip.SetActive(false);
                vinegar.transform.position = spawnPoint.position;
                vinegar.transform.rotation = Quaternion.identity;
                vinegar.SetActive(true);
                vinegar.GetComponent<XRGrabInteractable>().enabled = false;
                dropper2.GetComponent<XRGrabInteractable>().enabled = true;
                mission_Text.text = "將醋加入鋼棉";
                if (bag1_UI)
                {
                    bag1_UI.SetActive(true);
                    Destroy(bag1_UI, 7);
                }
                break;
            case Level5State.Bag2:
                vinegar.SetActive(false);
                dropper2.SetActive(false);
                bag2.SetActive(true);
                clip.SetActive(true);
                clip.transform.position = spawnPoint.position;
                clip.transform.rotation = Quaternion.Euler(0, 90, 0);
                steelWool2.transform.SetParent(null);
                steelWool2.tag = "SteelWool";
                steelWool2.GetComponent<Rigidbody>().isKinematic = false;
                mission_Text.text = "將鋼棉放進袋子中";
                if (vinegar_UI)
                {
                    vinegar_UI.SetActive(true);
                    Destroy(vinegar_UI, 7);
                }
                break;
            case Level5State.Test:
                if (bag2_UI)
                {
                    bag2_UI.SetActive(true);
                    Destroy(bag2_UI, 7);
                }
                clip.SetActive(false);
                steelWool_control.SetActive(false);
                steelWool_test.SetActive(false);
                bag1.SetActive(false);
                bag2.SetActive(false);
                mission_Text.transform.parent.gameObject.SetActive(false);
                part2.SetActive(false);
                questionPanel.SetActive(true);
                Quesion(0);
                break;
        }
    }

    public void ReturnLevelState(Level5State newState)
    {
        switch (newState)
        {
            case Level5State.Water:
                water.SetActive(false);
                water.transform.position = spawnPoint.position;
                water.transform.rotation = Quaternion.Euler(0, 90, 0);
                water.SetActive(true);
                break;
            case Level5State.Vinegar:
                vinegar.SetActive(false);
                vinegar.transform.position = spawnPoint.position;
                vinegar.transform.rotation = Quaternion.Euler(0, 90, 0);
                vinegar.SetActive(true);
                break;
        }
    }

    public void UpdateLevel5State_Int(int newState)
    {
        UpdateLevel5State((Level5State)newState);
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

        tests[0].text = questionData.explain[currentQusetIndex];
        currentQusetIndex++;
        ansPanel[0].SetActive(correctAns);
        ansPanel[1].SetActive(!correctAns);
        yield return new WaitForSeconds(2f);
        if (questionData.questions.Length == currentQusetIndex)
        {
            SceneManager.LoadScene("MainPage");
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