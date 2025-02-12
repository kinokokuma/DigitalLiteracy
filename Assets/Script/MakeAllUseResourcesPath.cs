using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine.UI;


public class MakeAllUseResourcesPath : MonoBehaviour
{
    // Start is called before the first frame update
    public void Start()
    {
        CreatePlayerCsv();
    }

    public void CreatePlayerCsv()
    {
        List<string[]> rowData = new List<string[]>();

        // Creating First row of titles manually..
        string[] rowDataTemp = new string[6];
        rowDataTemp[0] = "Name";
        rowDataTemp[1] = "Path";
        rowData.Add(rowDataTemp);

        string[] filePaths = Directory.GetFiles(Application.dataPath + "Resources/", " *.json");

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

        string filePath = Application.dataPath + "/Test/" + "AllUseResource" + ".csv";

        StreamWriter outStream = System.IO.File.CreateText(getPath());
        outStream.Write(sb);
        outStream.Close();

    }

    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + UserData.Story  + "_" + UserData.UserID + ".csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath  + "/CSV/" + UserData.Story + "_" + UserData.Solution+ "_" + UserData.UserID + ".csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath  + "/CSV/" + UserData.Story + "_" + UserData.Solution+ "_" + UserData.UserID + ".csv";
#else
        return   Application.persistentDataPath ;//"/CSV/" + UserData.Story + "_" + UserData.Solution+ "_" + UserData.UserID + ".csv";
#endif
        Debug.Log("get path leido");
    }
}
