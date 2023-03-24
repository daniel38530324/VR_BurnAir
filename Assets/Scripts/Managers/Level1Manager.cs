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

    [Header("Object")]
    [SerializeField] GameObject place;
    [SerializeField] GameObject candle_control, candle_test;

    Level1State level1State;

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
                place.SetActive(true);
                break;
            case Level1State.Fan:
                mission_Text.text = "用扇子搧火焰";
                candle_control.SetActive(true);
                candle_test.SetActive(true);
                break;
            case Level1State.Cover:
                mission_Text.text = "將火焰蓋住";
                break;
            case Level1State.Test:
                mission_Text.transform.parent.gameObject.SetActive(false);
                break;
        }
    }

    public void CloseExplain()
    {
        UpdateLevel1State(Level1State.Choose);
    }

}
