using UnityEngine;
using System.IO;

public static class CSVManager
{
    private static string reportDirectoryName = "Report"; //��Ƨ��W��
    private static string reportFileName = "report.csv"; //�ɮצW��
    private static string reportSeparator = ",";
    private static string[] reportHeaders = new string[1] { //�Ĥ@�C���D
        "id"
    };
    private static string timeStampHeader = "time stamp";

    #region Interactions

    public static void AppendToReport(string id, string[] strings) //�K�[�ɮ�
    {
        VerifyDirectory(); //�P�_��Ƨ��O�_�s�b
        VerifyFile(); //�P�_�ɮ׬O�_�s�b
        using (StreamWriter sw = File.AppendText(GetFilePath()))
        {
            string firstString = id + reportSeparator;
            string finalString = "";
            string result;
            for (int i = 0; i < strings.Length; i++)
            {
                if (finalString != "")
                {
                    finalString += reportSeparator;
                }
                if(strings[i] != "")
                {
                    finalString += strings[i];
                }
                
            }
            finalString += reportSeparator + GetTimeStamp();
            result = firstString + finalString;
            sw.WriteLine(result);
        }
    }

    public static void CreateReport() //�Ы��ɮ�
    {
        VerifyDirectory();
        using (StreamWriter sw = File.CreateText(GetFilePath()))
        {
            string finalString = "";
            for (int i = 0; i < reportHeaders.Length; i++)
            {
                if (finalString != "")
                {
                    finalString += reportSeparator;
                }
                finalString += reportHeaders[i];
            }
            //finalString += reportSeparator + timeStampHeader;
            sw.WriteLine(finalString);
        }
    }

    #endregion


    #region Operations

    static void VerifyDirectory()
    {
        string dir = GetDirectoryPath();
        if (!Directory.Exists(dir)) //�P�_��Ƨ��O�_�s�b
        {
            Directory.CreateDirectory(dir);
        }
    }

    static void VerifyFile()
    {
        string file = GetFilePath();
        if (!File.Exists(file)) //�P�_�ɮ׬O�_�s�b
        {
            CreateReport();
        }
    }

    #endregion


    #region Queries

    static string GetDirectoryPath() //���o��Ƨ����|
    {
        return Application.persistentDataPath + "/" + reportDirectoryName;
    }

    static string GetFilePath() //���o�ɮ׸��|
    {
        return GetDirectoryPath() + "/" + reportFileName;
    }

    static string GetTimeStamp() //���o�ɶ�
    {
        return (System.DateTime.Now.Year.ToString()) + "/" + (System.DateTime.Now.Month.ToString()) + "/" + (System.DateTime.Now.Day.ToString());
    }

    #endregion

}
