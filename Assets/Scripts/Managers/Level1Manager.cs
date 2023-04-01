using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Level1State
{
    Explain, //說明階段
    Choose,  //選擇器材階段
    Fan,     //搧扇子階段
    Cover,   //蓋住火焰階段
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
    [SerializeField] GameObject choose_UI, fan_UI, cover_UI;

    [Header("Object")]
    [SerializeField] GameObject place;
    [SerializeField] GameObject candle_control, candle_test, table;
    [SerializeField] GameObject fan;
    [SerializeField] GameObject cover;
    [SerializeField] Transform spawnPoint;

    Level1State level1State;
    
    [Header("Test")]
    public QuestionData questionData;
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
                table.GetComponent<Flashing>().StartGlinting();
                for (int i = 0; i < 6; i++)
                {
                    table.transform.GetChild(i).GetComponent<Flashing>().StartGlinting();
                }
                hint.SetActive(true);
                break;
            case Level1State.Fan:
                fan.transform.position = spawnPoint.position;
                cover.SetActive(false);
                mission_Text.text = "用扇子搧火焰";
                candle_control.SetActive(true);
                candle_test.SetActive(true);
                table.GetComponent<Flashing>().StopGlinting();
                for (int i = 0; i < 6; i++)
                {
                    table.transform.GetChild(i).GetComponent<Flashing>().StopGlinting();
                }
                choose_UI.SetActive(true);
                Destroy(choose_UI, 7);
                break;
            case Level1State.Cover:
                fan.SetActive(false);
                cover.SetActive(true);
                cover.transform.position = spawnPoint.position;
                mission_Text.text = "將火焰蓋住";
                fan_UI.SetActive(true);
                Destroy(fan_UI, 7);
                break;
            case Level1State.Test:
                cover.SetActive(false);
                candle_control.SetActive(false);
                candle_test.SetActive(false);
                mission_Text.transform.parent.gameObject.SetActive(false);
                questionPanel.SetActive(true);
                Quesion(0);
                cover_UI.SetActive(true);
                Destroy(cover_UI, 7);
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
            //Next level
            Debug.Log($"NextLevel");
        }else{
            Quesion(currentQusetIndex);
            ansPanel[0].SetActive(false);
            ansPanel[1].SetActive(false);
        }
    }
}
