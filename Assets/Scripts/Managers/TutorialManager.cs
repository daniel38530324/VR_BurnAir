using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [Header("GameManager")]
    [SerializeField] GameObject gameManager;

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
}
