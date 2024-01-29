using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum Level1State_PC
{
    Explain, //說明階段
    Choose,  //選擇器材階段
    Fan,     //搧扇子階段
    Cover,   //蓋住火焰階段
    Bucket,  //水桶階段
    Flour,   //麵粉階段
    Test     //測驗階段
}

public class Level1Manager_PC : MonoBehaviour
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
    [SerializeField] GameObject choose_UI, fan_UI, cover_UI, bucket_UI, flour_UI;

    [Header("Object")]
    [SerializeField] GameObject table;
    [SerializeField] GameObject fan, cover, bucket, flour, candle_control, candle_test, troch_control, troch_test, bowl, title;
    [SerializeField] Transform spawnPoint;
    [SerializeField] Candle_PC candle;
    [SerializeField] AudioSource fireSound_Troch, fireSound_Candle;

    public Level1State_PC level1State_PC;

    [SerializeField] GameObject[] finishs;
    
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
        
        table.GetComponent<Table>().Finished += CheckFinish;

        UpdateLevel1State(Level1State_PC.Explain);
    }

    private void Start()
    {
        AudioManager.Instance.PlayMusic("Scene");
    }

    private void Update()
    {
        levelTimer += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Tab)){
            hint_image.SetActive(!hint_image.activeSelf);
            hint_panel.SetActive(!hint_panel.activeSelf);
        }
    }

    public void UpdateLevel1State(Level1State_PC newState)
    {
        if(level1State_PC == newState) return;
        
        level1State_PC = newState;

        switch (newState)
        {
            case Level1State_PC.Explain:
                break;
            case Level1State_PC.Choose:
                mission_Text.transform.parent.gameObject.SetActive(true);
                mission_Text.text = "將正確的器材放在桌上";
                table.SetActive(true);
                //hint.SetActive(true);
                break;
            case Level1State_PC.Fan:
                mouseLook.RemoveThingOnHand();
                fireSound_Troch.volume = 0.8f;
                cover.SetActive(false);
                bucket.SetActive(false);
                flour.SetActive(false);
                mission_Text.text = "用扇子慢慢搧火焰, 點擊右鍵來使用物品";
                part2Panel.GetComponentInChildren<Text>().text = "用扇子慢慢搧火焰";
                troch_control.SetActive(true);
                troch_test.SetActive(true);
                table.SetActive(false);
                table.GetComponent<Flashing>().StopGlinting();
                fan.SetActive(true);
                fan.transform.position = spawnPoint.position;
                fan.transform.rotation = Quaternion.Euler(0, 90, 0);
                if(choose_UI)
                {
                    AudioManager.Instance.PlaySound("Level1_1");
                    choose_UI.SetActive(true);
                    Destroy(choose_UI, 11);
                }
                if(learningState[0])
                {
                    learningState[0] = false;
                    SendData("拿器材");
                }
                break;
            case Level1State_PC.Cover:
                candle.ReturnFire();
                mouseLook.RemoveThingOnHand();
                fireSound_Candle.volume = 0.8f;
                fan.SetActive(false);
                cover.SetActive(true);
                troch_control.SetActive(false);
                troch_test.SetActive(false);
                candle_control.SetActive(true);
                candle_test.SetActive(true);
                cover.transform.position = spawnPoint.position;
                cover.transform.localRotation = Quaternion.Euler(0, 90, 0);
                mission_Text.text = "將火焰蓋住";
                CheckFinish(4);
                if(fan_UI)
                {
                    AudioManager.Instance.PlaySound("Level1_2");
                    fan_UI.SetActive(true);
                    Destroy(fan_UI, 11);
                }
                if(learningState[1])
                {
                    learningState[1] = false;
                    SendData("扇子搧火");
                }
                break;
            case Level1State_PC.Bucket:
                fireSound_Candle.volume = 0.8f;
                candle.ReturnFire();
                mouseLook.RemoveThingOnHand();
                cover.SetActive(false);
                bucket.transform.position = spawnPoint.position;
                bucket.transform.rotation = Quaternion.identity;
                bucket.SetActive(true);
                bowl.SetActive(true);
                mission_Text.text = "將火焰澆熄, 點擊右鍵來使用物品";
                part2Panel.GetComponentInChildren<Text>().text = "將火焰澆熄, 點擊右鍵來使用物品";
                CheckFinish(5);
                if(cover_UI)
                {
                    AudioManager.Instance.PlaySound("Level1_3");
                    cover_UI.SetActive(true);
                    Destroy(cover_UI, 11);
                }
                if(learningState[2])
                {
                    learningState[2] = false;
                    SendData("蓋子滅火");
                }
                break;
            case Level1State_PC.Flour:
                fireSound_Candle.volume = 0.8f;
                candle.ReturnFire();
                mouseLook.RemoveThingOnHand();
                bucket.SetActive(false);           
                flour.transform.position = spawnPoint.position;
                flour.transform.rotation = Quaternion.identity;
                flour.SetActive(true);
                bowl.transform.GetChild(0).gameObject.SetActive(false);
                mission_Text.text = "將麵粉加入火中";
                part2Panel.GetComponentInChildren<Text>().text = "將麵粉加入火中";
                CheckFinish(6);
                if(bucket_UI)
                {
                    AudioManager.Instance.PlaySound("Level1_4");
                    bucket_UI.SetActive(true);
                    Destroy(bucket_UI, 11);
                }
                if(learningState[3])
                {
                    learningState[3] = false;
                    SendData("用水滅火");
                }
                break;
            case Level1State_PC.Test:
                mouseLook.RemoveThingOnHand();
                title.SetActive(false);
                flour.SetActive(false);
                candle_control.SetActive(false);
                candle_test.SetActive(false);
                mission_Text.text = "前往黑板進行測驗";
                part2Panel.SetActive(false);
                questionPanel.SetActive(true);
                Quesion(0);
                AudioManager.Instance.PlaySound("Level1_5");
                flour_UI.SetActive(true);
                Destroy(flour_UI, 15);
                CheckFinish(7);
                if (table.TryGetComponent<Table>(out Table item))
                {
                    item.Finished -= CheckFinish;
                }
                if(learningState[4])
                {
                    learningState[4] = false;
                    SendData("麵粉加入火");
                }
                break;
        }
    }

    public void ReturnLevelState(Level1State_PC newState)
    {
        switch (newState)
        {
            case Level1State_PC.Fan:
                fan.SetActive(false);
                fan.transform.position = spawnPoint.position;
                fan.transform.rotation = Quaternion.Euler(0, 90, 0);
                fan.SetActive(true);
                break;
            case Level1State_PC.Cover:
                cover.SetActive(false);
                cover.transform.position = spawnPoint.position;
                cover.transform.localRotation = Quaternion.Euler(0, 90, 0);
                cover.SetActive(true);
                break;
            case Level1State_PC.Bucket:
                bucket.SetActive(false);
                bucket.transform.position = spawnPoint.position;
                bucket.transform.rotation = Quaternion.identity;
                bucket.SetActive(true);
                break;
            case Level1State_PC.Flour:
                flour.SetActive(false);
                flour.transform.position = spawnPoint.position;
                flour.transform.rotation = Quaternion.identity;
                flour.SetActive(true);
                break;
        }
    }

    public void UpdateLevel1State_PC_Int(int newState)
    {
        UpdateLevel1State((Level1State_PC)newState);
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
            Cursor.lockState = CursorLockMode.Confined;
            GameManager.levelState[0] = true;
            SceneManager.LoadScene("MainPage_PC");
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

    public void CheckFinish(int index)
    {
        finishs[index].SetActive(true);
    }
}
