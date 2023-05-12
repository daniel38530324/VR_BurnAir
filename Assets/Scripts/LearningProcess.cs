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
    [SerializeField] GameObject login, project;

    public void DEV_AppendToReport() //�n�Q�I�s�� Method
    {
        CSVManager.AppendToReport( //��J�n�g�J���ɮ�
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
            login.SetActive(false);
            project.SetActive(true);
        }
        else
        {
            login.SetActive(true);
            project.SetActive(false);
        }
    }
}
