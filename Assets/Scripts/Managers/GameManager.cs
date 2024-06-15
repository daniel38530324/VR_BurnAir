using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static bool[] levelState = new bool[6];

    [SerializeField] GameObject[] finishs; 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(SceneManager.GetActiveScene().name == "MainPage" || SceneManager.GetActiveScene().name == "MainPage_PC")
        {
            AudioManager.Instance.PlayMusic("MainPage");
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void CheckState()
    {
        for(int i = 0; i < finishs.Length; i++)
        {
            if (levelState[i])
            {
                finishs[i].SetActive(true);
            }
        }
    }

    public void GoToApp()
    {
        Application.OpenURL("viversebusiness://");
    }

    public void GoToVR()
    {
        Application.OpenURL("EducationMarket://Project_112-V02");
    }
}
