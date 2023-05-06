using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit;

public enum Level4State
{
    Explain,       //說明階段
    Choose,        //選擇器材階段
    CaCO3,         //放入大理石階段
    Water,         //加入水階段
    HCl,           //加入鹽酸階段
    Tube,          //管子階段
    Cover,         //放入杯子階段
    PickUp,        //拿起階段
    IncenseSticks, //線香測試階段
    Test           //測驗階段
}

public class Level4Manager : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameObject gameManager;

    [Header("Mission")]
    [SerializeField] Text mission_Text;

    [Header("Knowledge points")]
    [SerializeField] GameObject choose_UI;
    [SerializeField] GameObject hcl_UI, cover_UI, pickUp_UI, incenseSticks_UI;

    [Header("Object")]
    [SerializeField] GameObject waterTank;
    [SerializeField] GameObject suctionBottle, table, waterBucket, cover, caco3, hcl, dropper, pipe, bottle, incenseSticks, bottleForIncenseSticks;
    [SerializeField] GameObject[] caco3s;

    [SerializeField] Transform spawnPoint;

    Level4State level4State;

    [Header("Test")]
    [SerializeField] GameObject part2;
    [SerializeField] GameObject questionPanel;
    public QuestionData questionData;
    public Text[] tests;
    public GameObject[] ansPanel;
    int currentQusetIndex;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }

        UpdateLevel4State(Level4State.Explain);
    }

    public void UpdateLevel4State(Level4State newState)
    {
        level4State = newState;

        switch (newState)
        {
            case Level4State.Explain:

                break;
            case Level4State.Choose:
                mission_Text.transform.parent.gameObject.SetActive(true);
                mission_Text.text = "將正確的器材放在桌上";
                table.SetActive(true);
                break;
            case Level4State.CaCO3:
                choose_UI.SetActive(true);
                Destroy(choose_UI, 5);
                mission_Text.text = "加入大理石";
                table.SetActive(false);
                waterBucket.SetActive(false);
                hcl.SetActive(false);
                cover.SetActive(false);
                caco3.transform.position = spawnPoint.position;
                caco3.transform.rotation = Quaternion.Euler(0, 0, 0);
                caco3.SetActive(true);
                for(int i = 0; i <= 9; i++)
                {
                    
                }
                waterTank.SetActive(true);
                suctionBottle.SetActive(true);
                break;
            case Level4State.Water:
                mission_Text.text = "加入水";
                caco3.SetActive(false);
                waterBucket.transform.position = spawnPoint.position;
                waterBucket.transform.rotation = Quaternion.Euler(0, -90, 0);
                waterBucket.SetActive(true);
                waterBucket.GetComponent<WaterBucket_New>().enabled = true;
                break;
            case Level4State.HCl:
                mission_Text.text = "加入鹽酸";
                waterBucket.SetActive(false);
                hcl.transform.position = spawnPoint.position;
                hcl.transform.rotation = Quaternion.Euler(0, -180, 0);
                hcl.SetActive(true);
                hcl.GetComponent<XRGrabInteractable>().enabled = false;
                dropper.GetComponent<XRGrabInteractable>().enabled = true;
                break;
            case Level4State.Tube:
                hcl_UI.SetActive(true);
                Destroy(hcl_UI, 5);
                mission_Text.text = "放入管子";
                hcl.SetActive(false);
                dropper.SetActive(false);
                pipe.SetActive(true);
                break;
            case Level4State.Cover:
                mission_Text.text = "放入杯子";
                bottle.SetActive(true);
                break;
            case Level4State.PickUp:
                cover_UI.SetActive(true);
                Destroy(cover_UI, 5);
                mission_Text.text = "拿起杯子";
                break;
            case Level4State.IncenseSticks:
                incenseSticks.SetActive(true);
                bottleForIncenseSticks.SetActive(true);
                mission_Text.text = "拿下戴玻片並用線香測試";
                break;
            case Level4State.Test:
                incenseSticks_UI.SetActive(true);
                Destroy(incenseSticks_UI, 5);
                incenseSticks.SetActive(false);
                bottleForIncenseSticks.SetActive(false);
                waterTank.SetActive(false);
                suctionBottle.SetActive(false);
                mission_Text.transform.parent.gameObject.SetActive(false);
                part2.SetActive(false);
                questionPanel.SetActive(true);
                Quesion(0);
                break;
        }
    }

    public void UpdateLevel4State_Int(int newState)
    {
        UpdateLevel4State((Level4State)newState);
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
        tests[0].text = questionData.explain[currentQusetIndex];
        currentQusetIndex++;
        ansPanel[0].SetActive(correctAns);
        ansPanel[1].SetActive(!correctAns);
        yield return new WaitForSeconds(2f);
        if (questionData.questions.Length == currentQusetIndex)
        {
            //SceneManager.LoadScene("Level3");
        }
        else
        {
            Quesion(currentQusetIndex);
            ansPanel[0].SetActive(false);
            ansPanel[1].SetActive(false);
        }
    }

    public void GetCaCO3()
    {
        if(level4State == Level4State.CaCO3)
        {
            StartCoroutine(CaCO3());
            
        }
    }

    IEnumerator CaCO3()
    {
        yield return new WaitForSeconds(2);
        for (int i = 0; i <= 9; i++)
        {
            //caco3s[i].transform.parent = null;
            caco3s[i].GetComponent<Rigidbody>().isKinematic = false;
            caco3s[i].gameObject.layer = 9;
        }
    }
}
