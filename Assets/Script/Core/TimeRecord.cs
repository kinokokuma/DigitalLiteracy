using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;

public class TimeRecord : MonoSingleton<TimeRecord>
{
    private string csv_name = "Story1_";
    private int taskNumber = 1;
    private string id = "1";
    private List<string[]> rowData = new List<string[]>();
    private List<string[]> logData = new List<string[]>();

    public void Start()
    {
    }

    public void AddTaskNumber()
    {
        taskNumber++;
    }

    public void CreatePlayerCsv()
    {
        List<string[]> rowData = new List<string[]>();

        // Creating First row of titles manually..
        string[] rowDataTemp = new string[6];
        rowDataTemp[0] = "Date";
        rowDataTemp[1] = "TimeFromStartQuestTion";
        rowDataTemp[2] = "ID";
        rowDataTemp[3] = "AnsTime";
        rowDataTemp[4] = "Ans";
        rowDataTemp[5] = "IsSignificant";
        rowData.Add(rowDataTemp);

        string[][] output = new string[rowData.Count][];


        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";
        string dataString = string.Empty;

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
        {
            sb.AppendLine(string.Join(delimiter, output[index]));
        }

        string filePath = Application.dataPath + "/CSV/" + UserData.Story +  "_" + UserData.UserID + ".csv";
        if (!File.Exists(getPath()))
        {
            StreamWriter outStream = System.IO.File.CreateText(getPath());
            outStream.Write(sb);
            outStream.Close();
        }
        
      //  File.WriteAllText(getPath(), sb.ToString());
       // File.WriteAllBytes(getPath(), Encoding.ASCII.GetBytes(sb.ToString()));
    }

    private string[] rowDataTemp = new string[6];

    public void SaveRecord(string ID, string Ans, float startTime,bool isSignificant =false)
    {
        rowData = new List<string[]>();

        // Creating First row of titles manually..

        DateTime serverTime = DateTime.Now; // gives you current Time in server timeZone
        long unixTime = ((DateTimeOffset)serverTime).ToUnixTimeMilliseconds();

        rowDataTemp[0] = unixTime.ToString();
        rowDataTemp[1] = startTime.ToString();
        rowDataTemp[2] = "choice-"+ID.ToString();
        rowDataTemp[3] = (Time.time - startTime).ToString();
        rowDataTemp[4] = Ans.Replace("\n"," ");
        rowDataTemp[5] = isSignificant.ToString();
        rowData.Add(rowDataTemp);

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));

        string filePath = Application.dataPath + "/CSV/" + UserData.Story+ "_" + UserData.UserID + ".csv";
        StreamWriter outStream = System.IO.File.AppendText(getPath());
        outStream.Write(sb);
        outStream.Close();
        
        //File.WriteAllText(getPath(), sb.ToString());
        //File.WriteAllBytes(getPath(), Encoding.ASCII.GetBytes(sb.ToString()));

    }
    string[] LogDataTemp = new string[1];

    public void LogCheck(string ID)
    {
        logData = new List<string[]>();


        LogDataTemp[0] = ID;
        logData.Add(LogDataTemp);

        string[][] output = new string[logData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = logData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));

         string filePath = Application.dataPath + "/CSV/" + UserData.Story + "_" + UserData.UserID + "_Log.csv";
         StreamWriter outStream = System.IO.File.AppendText(getPath("_LOG"));
         outStream.Write(sb);
         outStream.Close();

    }

    void Update()
    {
      
    }


    // Following method is used to retrive the relative path as device platform
    private string getPath(string subPath = "")
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + UserData.Story + "_" + UserData.UserID+ subPath + ".csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath  + "/CSV/" + UserData.Story +  "_" + UserData.UserID+subPath + ".csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath  + "/" + UserData.Story + "_" + UserData.UserID+subPath + ".csv";
#else
        return   Application.persistentDataPath"/CSV/" + UserData.Story  + "_" + UserData.UserID + ".csv";
#endif
        Debug.Log("get path leido");
    }
}
