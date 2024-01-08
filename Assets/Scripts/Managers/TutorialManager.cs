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
                hint.text = "�ɭˤ���˥X";
                break;
            case "Paint Gun":
                hint.text = "���U�O����Q�x";
                break;
            case "Flour":
                hint.text = "�ɭ��ѯ��˥X";
                break;
            case "H2O2":
                hint.text = "�ɭ�������˥X";
                break;
            case "Mushroom":
                hint.text = "�ɭ˪��wۣ�˥X";
                break;
            case "Lemonade":
                hint.text = "���U�O����Q�x";
                break;
            case "Dropper":
                hint.text = "���U�O�������X";
                break;
            case "Fan":
                hint.text = "ݵ�ʮ��l�j��";
                break;
        }
        hint.gameObject.SetActive(true);
    }
}
