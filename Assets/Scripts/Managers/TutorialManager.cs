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
                hint.text = "¶É­Ë¤ô±í­Ë¥X";
                break;
            case "Paint Gun":
                hint.text = "«ö¤UªO¾÷Áä¼QÅx";
                break;
            case "Flour":
                hint.text = "¶É­ËÄÑ¯»­Ë¥X";
                break;
            case "H2O2":
                hint.text = "¶É­ËÂù®ñ¤ô­Ë¥X";
                break;
            case "Mushroom":
                hint.text = "¶É­Ëª÷°wÛ£­Ë¥X";
                break;
            case "Lemonade":
                hint.text = "«ö¤UªO¾÷Áä¼QÅx";
                break;
            case "Dropper":
                hint.text = "«ö¤UªO¾÷ÁäÀ½¥X";
                break;
            case "Fan":
                hint.text = "Ýµ°Ê®°¤l§j­·";
                break;
        }
        hint.gameObject.SetActive(true);
    }
}
