using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class LearningProcess : MonoBehaviour
{
    public static string user = "";
    public static string[] data = { "", "", "", "" };

    [SerializeField] InputField userText;
    [SerializeField] GameObject title, login, project;

    public void DEV_AppendToReport() //要被呼叫的 Method
    {
        CSVManager.AppendToReport( //放入要寫入的檔案
            user,
            data
        );
        Debug.Log("<color=green>Report updated successfully!</color>");

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = "";
        }
    }

    public void Login()
    {
        user = userText.text;
        //data[0] = "" + DateTime.Now;
        DEV_AppendToReport();
    }

    public void CheckLogin()
    {
        if (user != "")
        {
            title.SetActive(false);
            login.SetActive(false);
            project.SetActive(true);
        }
        else
        {
            title.SetActive(false);
            login.SetActive(true);
            project.SetActive(false);
        }
    }
}