using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public enum Level4State_New
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

public class Level4Manager_New : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameObject gameManager;

    [Header("LearningProcess")]
    [SerializeField] LearningProcess learningProcess;

    [Header("Mission")]
    [SerializeField] Text mission_Text;

    [Header("Knowledge points")]
    [SerializeField] GameObject choose_UI;
    [SerializeField] GameObject soda_UI, vinegar_UI, plasticBag_UI, glassCover_UI, shake_UI, incenseSticks_UI;

    [Header("Object")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject table, soda, vinegar, cover, cover2, glassCover, glassCover2, incenseSticks, incenseSticksTest, plasticBag, plasticBag2, plasticBag_CO2, plasticBag_limeWater, limeWater, dropper, title;
    [SerializeField] GameObject[] mushrooms;

    public Level4State_New level4State;
    float levelTimer = 0;

    [SerializeField] Transform[] equipmentPoints;
    [SerializeField] GameObject[] finishs;

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
        table.GetComponent<Table>().Finished += CheckFinish;
        UpdateLevel4State(Level4State_New.Explain);
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic("Scene");
    }

    private void Update()
    {
        levelTimer += Time.deltaTime;
    }

    public void UpdateLevel4State(Level4State_New newState)
    {
        level4State = newState;

        switch (newState)
        {
            case Level4State_New.Explain:

                break;
            case Level4State_New.Choose:
                mission_Text.transform.parent.gameObject.SetActive(true);
                mission_Text.text = "將正確的器材放在桌上";
                table.SetActive(true);
                break;
            case Level4State_New.Soda:
                AudioManager.Instance.PlaySound("Level4_1");
                choose_UI.SetActive(true);
                Destroy(choose_UI, 11);
                mission_Text.text = "加入小蘇打粉";
                part2.GetComponentInChildren<Text>().text = "加入小蘇打粉";
                table.SetActive(false);
                vinegar.SetActive(false);
                cover.SetActive(false);
                limeWater.SetActive(false);
                soda.transform.position = spawnPoint.position;
                soda.transform.rotation = Quaternion.Euler(0, 0, 0);
                soda.SetActive(true);
                soda.GetComponent<WaterBucket_New>().enabled = true;
                cover2.SetActive(true);
                if (learningState[0])
                {
                    learningState[0] = false;
                    SendData("拿器材");
                }
                break;
            case Level4State_New.Vinegar:
                //soda_UI.SetActive(true);
                //Destroy(soda_UI, 5);
                mission_Text.text = "加入醋";
                part2.GetComponentInChildren<Text>().text = "加入醋";
                soda.SetActive(false);
                vinegar.transform.position = spawnPoint.position;
                vinegar.transform.rotation = Quaternion.Euler(0, 0, 0);
                vinegar.SetActive(true);
                vinegar.GetComponent<XRGrabInteractable>().enabled = false;
                dropper.GetComponent<XRGrabInteractable>().enabled = true;
                CheckFinish(4);
                if (learningState[1])
                {
                    learningState[1] = false;
                    SendData("加入小蘇打粉");
                }
                break;
            case Level4State_New.PlasticBag:
                AudioManager.Instance.PlaySound("Level4_2");
                vinegar_UI.SetActive(true);
                Destroy(vinegar_UI, 11);
                mission_Text.text = "套住塑膠袋";
                part2.GetComponentInChildren<Text>().text = "套住塑膠袋";
                vinegar.SetActive(false);
                dropper.SetActive(false);
                plasticBag.SetActive(true);
                CheckFinish(5);
                CheckFinish(6);
                if (learningState[2])
                {
                    learningState[2] = false;
                    SendData("加入醋");
                }
                break;
            case Level4State_New.GlassCover:
                AudioManager.Instance.PlaySound("Level4_3");
                plasticBag_UI.SetActive(true);
                Destroy(plasticBag_UI, 11);
                mission_Text.text = "放上透明板";
                part2.GetComponentInChildren<Text>().text = "放上透明板";
                plasticBag.SetActive(false);
                plasticBag2.SetActive(false);
                plasticBag_CO2.SetActive(true);
                glassCover.SetActive(true);
                CheckFinish(7);
                if (learningState[3])
                {
                    learningState[3] = false;
                    SendData("套住塑膠袋");
                }
                break;
            case Level4State_New.LimeWater:
                AudioManager.Instance.PlaySound("Level4_4");
                glassCover_UI.SetActive(true);
                Destroy(glassCover_UI, 11);
                mission_Text.text = "將澄清石灰水加到塑膠袋";
                part2.GetComponentInChildren<Text>().text = "將澄清石灰水加到塑膠袋";
                limeWater.transform.position = spawnPoint.position;
                limeWater.transform.rotation = Quaternion.Euler(0, 0, 0);
                limeWater.SetActive(true);
                CheckFinish(8);
                if (learningState[4])
                {
                    learningState[4] = false;
                    SendData("放上玻璃蓋");
                }
                break;
            case Level4State_New.Shake:
                mission_Text.text = "搖晃塑膠袋";
                part2.GetComponentInChildren<Text>().text = "搖晃塑膠袋";
                plasticBag_CO2.SetActive(false);
                limeWater.SetActive(false);
                CheckFinish(9);
                if (learningState[5])
                {
                    learningState[5] = false;
                    SendData("加入澄清石灰水");
                }
                break;
            case Level4State_New.IncenseSticks:
                AudioManager.Instance.PlaySound("Level4_5");
                shake_UI.SetActive(true);
                Destroy(shake_UI, 11);
                mission_Text.text = "使用線香測試";
                part2.GetComponentInChildren<Text>().text = "使用線香測試";
                plasticBag_limeWater.SetActive(false);
                incenseSticks.SetActive(true);
                incenseSticksTest.SetActive(true);
                CheckFinish(10);
                if (learningState[6])
                {
                    learningState[6] = false;
                    SendData("搖晃塑膠袋");
                }
                break;
            case Level4State_New.Test:
                title.SetActive(false);
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
                CheckFinish(11);
                if (table.TryGetComponent<Table>(out Table item))
                {
                    item.Finished -= CheckFinish;
                }
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
        UpdateLevel4State((Level4State_New)newState);
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
        yield return new WaitForSeconds(5f);
        if (questionData.questions.Length == currentQusetIndex)
        {
            GameManager.levelState[3] = true;
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

    public void GetMushroom()
    {
        if (level4State == Level4State_New.Soda)
        {
            StartCoroutine(Mushroom());

        }
    }

    IEnumerator Mushroom()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i <= 9; i++)
        {
            //mushrooms[i].transform.parent = null;
            mushrooms[i].GetComponent<Rigidbody>().isKinematic = false;
            mushrooms[i].gameObject.layer = 9;
        }
    }
    
    public void RetuenPosition(Transform equipment)
    {
        switch (level4State)
        {
            case Level4State_New.Explain:
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
                break;
            case Level4State_New.Choose:
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
                break;
            default:
                equipment.GetComponent<Rigidbody>().isKinematic = true;

                equipment.position = spawnPoint.position;
                soda.transform.rotation = Quaternion.Euler(0, 0, 0);
                vinegar.transform.rotation = Quaternion.Euler(0, 0, 0);
                limeWater.transform.rotation = Quaternion.Euler(0, 0, 0);

                equipment.GetComponent<Rigidbody>().isKinematic = false;
                break;
        }
    }

    public void RetuenPosition_Others(Transform equipment)
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
}
