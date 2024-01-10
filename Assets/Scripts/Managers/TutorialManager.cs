using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameObject gameManager;

    [Header("Object")]
    [SerializeField] Text hint;

    private void Awake()
    {
        if (GameManager.instance == null)
        {
            Instantiate(gameManager);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic("Scene");
    }

    public void ReturnMainPage()
    {
        GameManager.instance.ChangeScene("MainPage");
    }

    public void GetHint(string name)
    {
        switch(name)
        {
            case "Water Bucket":
                hint.text = "傾倒水桶倒出";
                break;
            case "Paint Gun":
                hint.text = "按下板機鍵噴灑";
                break;
            case "Flour":
                hint.text = "傾倒麵粉倒出";
                break;
            case "H2O2":
                hint.text = "傾倒雙氧水倒出";
                break;
            case "Mushroom":
                hint.text = "傾倒金針菇倒出";
                break;
            case "Lemonade":
                hint.text = "按下板機鍵噴灑";
                break;
            case "Dropper":
                hint.text = "按下板機鍵擠出";
                break;
            case "Fan":
                hint.text = "搧動扇子吹風";
                break;
        }
        hint.gameObject.SetActive(true);
    }
}
