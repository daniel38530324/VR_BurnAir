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

    Level1State level1State;
    
    [Header("Test")]
    public QuestionData questionData;
    public GameObject part2Panel;
    public GameObject questionPanel;
    public Text[] tests;
    public GameObject[] ansPanel;
    int currentQusetIndex;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        UpdateLevel1State(Level1State.Explain);
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
                mission_Text.text = "用扇子搧火焰";
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
        tests[0].text = questionData.explain[currentQusetIndex];
        currentQusetIndex++;
        ansPanel[0].SetActive(correctAns);
        ansPanel[1].SetActive(!correctAns);
        yield return new WaitForSeconds(2f);
        if(questionData.questions.Length == currentQusetIndex){
            SceneManager.LoadScene("Level2");
        }else{
            Quesion(currentQusetIndex);
            ansPanel[0].SetActive(false);
            ansPanel[1].SetActive(false);
        }
    }
}
