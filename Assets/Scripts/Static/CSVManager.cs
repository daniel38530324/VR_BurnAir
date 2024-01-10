using UnityEngine;
using System.IO;

public static class CSVManager
{
    private static string reportDirectoryName = "Report"; //資料夾名稱
    private static string reportFileName = "report.csv"; //檔案名稱
    private static string reportSeparator = ",";
    private static string[] reportHeaders = new string[1] { //第一列標題
        "id"
    };
    private static string timeStampHeader = "time stamp";

    #region Interactions

    public static void AppendToReport(string id, string[] strings) //添加檔案
    {
        VerifyDirectory(); //判斷資料夾是否存在
        VerifyFile(); //判斷檔案是否存在
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

    public static void CreateReport() //創建檔案
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
        if (!Directory.Exists(dir)) //判斷資料夾是否存在
        {
            Directory.CreateDirectory(dir);
        }
    }

    static void VerifyFile()
    {
        string file = GetFilePath();
        if (!File.Exists(file)) //判斷檔案是否存在
        {
            CreateReport();
        }
    }

    #endregion


    #region Queries

    static string GetDirectoryPath() //取得資料夾路徑
    {
        return Application.persistentDataPath + "/" + reportDirectoryName;
    }

    static string GetFilePath() //取得檔案路徑
    {
        return GetDirectoryPath() + "/" + reportFileName;
    }

    static string GetTimeStamp() //取得時間
    {
        return (System.DateTime.Now.Year.ToString()) + "/" + (System.DateTime.Now.Month.ToString()) + "/" + (System.DateTime.Now.Day.ToString());
    }

    #endregion

}
