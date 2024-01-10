using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public enum Level6State_PC
{
    Explain,       //說明階段
    Choose,        //選擇器材階段
    Lemonade,      //噴灑檸檬水階段
    Rag,           //抹布擦拭階段
    WD40,          //噴灑WD40到關節階段
    PaintGun,      //噴灑噴漆到機器人階段
    PlasticSleeve, //使用塑膠套套住腳階段
    Test           //測驗階段
}

public class Level6Manager_PC : MonoBehaviour
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
    [SerializeField] GameObject lemonade_UI, rag_UI, wd40_UI, paintGun_UI, plasticSleeve_UI;

    [Header("Object")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] GameObject table, lemonade, rag, wd40, paintGun, plasticSleeve, robot, robot_collider, joint_Collider, headBody_Collider, footCollider, title;

    public Level6State_PC level6State;
    float levelTimer = 0;
    
    [SerializeField] GameObject[] finishs;

    [Header("Test")]
    [SerializeField] GameObject part2;
    [SerializeField] GameObject questionPanel;
    public QuestionData questionData;
    public Text[] tests;
    public GameObject[] ansPanel;
    public event Action OnWD40Num;
    public event Action OnPlasticSleeveNum;
    public event Action OnPaintGunNum;
    int currentQusetIndex;
    int number = 0;

    bool[] learningState = { true, true, true, true, true, true};

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        table.GetComponent<Table>().Finished += CheckFinish;

        UpdateLevel6State(Level6State_PC.Explain);
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

    public void UpdateLevel6State(Level6State_PC newState)
    {
        if(level6State == newState) return;
        
        level6State = newState;

        switch (newState)
        {
            case Level6State_PC.Explain:

                break;
            case Level6State_PC.Choose:
                mission_Text.transform.parent.gameObject.SetActive(true);
                mission_Text.text = "將正確的器材放在桌上";
                table.SetActive(true);
                break;
            case Level6State_PC.Lemonade:
                AudioManager.Instance.PlaySound("Level6_1");
                mouseLook.RemoveThingOnHand();
                choose_UI.SetActive(true);
                Destroy(choose_UI, 11);
                mission_Text.text = "噴灑檸檬水至機器人";
                part2.GetComponentInChildren<Text>().text = "噴灑檸檬水至機器人";
                table.SetActive(false);
                rag.SetActive(false);
                wd40.SetActive(false);
                paintGun.SetActive(false);
                plasticSleeve.SetActive(false);
                lemonade.transform.position = spawnPoint.position;
                lemonade.transform.rotation = Quaternion.Euler(0, 0, 0);
                lemonade.SetActive(true);
                robot.SetActive(true);
                robot_collider.SetActive(true);        
                if (learningState[0])
                {
                    learningState[0] = false;
                    SendData("拿器材");
                }
                break;
            case Level6State_PC.Rag:
                //lemonade_UI.SetActive(true);
                //Destroy(lemonade_UI, 5);
                mission_Text.text = "抹布擦拭機器人";
                part2.GetComponentInChildren<Text>().text = "抹布擦拭機器人";
                mouseLook.RemoveThingOnHand();
                lemonade.SetActive(false);
                rag.transform.position = spawnPoint.position;
                rag.transform.rotation = Quaternion.Euler(0, 0, 0);
                rag.SetActive(true);      
                CheckFinish(5); 
                if (learningState[1])
                {
                    learningState[1] = false;
                    SendData("噴灑檸檬水至機器人");
                }
                break;
            case Level6State_PC.WD40:
                AudioManager.Instance.PlaySound("Level6_2");
                rag_UI.SetActive(true);
                Destroy(rag_UI, 11);
                mission_Text.text = "潤滑液噴灑至機器人關節 0/5";
                part2.GetComponentInChildren<Text>().text = "潤滑液噴灑至機器人關節";
                mouseLook.RemoveThingOnHand();
                rag.SetActive(false);
                robot_collider.SetActive(false);
                wd40.transform.position = spawnPoint.position;
                wd40.transform.rotation = Quaternion.Euler(0, 0, 0);
                wd40.SetActive(true);
                joint_Collider.SetActive(true);
                CheckFinish(6);
                if (learningState[2])
                {
                    learningState[2] = false;
                    SendData("抹布擦拭機器人");
                }
                break;
            case Level6State_PC.PaintGun:
                wd40_UI.SetActive(true);
                Destroy(wd40_UI, 11);
                mission_Text.text = "噴漆槍噴灑至機器人 0/2";
                part2.GetComponentInChildren<Text>().text = "噴漆槍噴灑至機器人";
                mouseLook.RemoveThingOnHand();
                wd40.SetActive(false);
                joint_Collider.SetActive(false);
                paintGun.transform.position = spawnPoint.position;
                paintGun.transform.rotation = Quaternion.Euler(0, 0, 0);
                paintGun.SetActive(true);
                headBody_Collider.SetActive(true);
                CheckFinish(7);
                if (learningState[3])
                {
                    learningState[3] = false;
                    SendData("潤滑液噴灑至機器人關節");
                }               
                break;
            case Level6State_PC.PlasticSleeve:
                AudioManager.Instance.PlaySound("Level6_4");
                paintGun_UI.SetActive(true);
                Destroy(paintGun_UI, 11);
                mission_Text.text = "塑膠套套住機器人的腳 0/2";
                part2.GetComponentInChildren<Text>().text = "塑膠套套住機器人的腳";
                mouseLook.RemoveThingOnHand();
                paintGun.SetActive(false);
                headBody_Collider.SetActive(false);
                plasticSleeve.transform.position = spawnPoint.position;
                plasticSleeve.transform.rotation = Quaternion.Euler(0, 0, 0);
                plasticSleeve.SetActive(true);
                footCollider.SetActive(true);   
                CheckFinish(8);         
                if (learningState[4])
                {
                    learningState[4] = false;
                    SendData("噴漆槍噴灑至機器人");
                }             
                break;
            case Level6State_PC.Test:
                title.SetActive(false);
                AudioManager.Instance.PlaySound("Level6_5");
                plasticSleeve_UI.SetActive(true);
                Destroy(plasticSleeve_UI, 11);
                mouseLook.RemoveThingOnHand();
                mission_Text.transform.parent.gameObject.SetActive(false);
                plasticSleeve.SetActive(false);
                robot.SetActive(false);
                part2.SetActive(false);
                questionPanel.SetActive(true);
                Quesion(0);
                CheckFinish(9);
                if (table.TryGetComponent<Table>(out Table item))
                {
                    item.Finished -= CheckFinish;
                }
                if (learningState[5])
                {
                    learningState[5] = false;
                    SendData("橡膠套套住機器人");
                }             
                break;
        }
    }

    public void UpdateLevel6State_Int(int newState)
    {
        UpdateLevel6State((Level6State_PC)newState);
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
        LearningProcess.data[0] = "單元六";
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
            GameManager.levelState[5] = true;
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
        LearningProcess.data[0] = "單元六";
        LearningProcess.data[1] = things;
        LearningProcess.data[2] = success ? "成功" : "失敗";
        LearningProcess.data[3] = levelTimer.ToString("0");
        learningProcess.DEV_AppendToReport();
    }

    public void SendChooseFailData()
    {
        SendData("拿器材", false);
    }

    public void UpdateNum(Level6State_PC currentState)
    {
        number++;
        if(currentState == Level6State_PC.WD40)
        {
            if (number >= 5)
            {
                number = 5;
                OnWD40Num.Invoke();
            }
            mission_Text.text = "潤滑液噴灑至機器人關節" + number + "/5";
        }
        else if (currentState == Level6State_PC.PaintGun)
        {
            if (number >= 2)
            {
                number = 2;
                OnPaintGunNum.Invoke();
            }
            mission_Text.text = "塑膠套套住機器人" + number + "/2";
        }
        else if (currentState == Level6State_PC.PlasticSleeve)
        {
            if (number >= 2)
            {
                number = 2;
                OnPlasticSleeveNum.Invoke();
            }
            mission_Text.text = "塑膠套套住機器人的腳"  + number + "/2";
        }
    }
    
    public void CheckFinish(int index)
    {
        finishs[index].SetActive(true);
    }
}
