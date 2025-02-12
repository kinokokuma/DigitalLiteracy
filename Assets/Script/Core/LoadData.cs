using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;


public class LoadData : MonoBehaviour
{
    private string url = "https://drive.google.com/uc?export=download&id=";
    private string jsonURL = "https://drive.google.com/uc?export=download&id=1B_YC58hLS9wa3WP8cBvWHlleAXfK01i6";
    ImageUrl data;
    void Start()
    {
       // string json = Resources.Load<TextAsset>("DataPath").ToString();
       // data = JsonUtility.FromJson<ImageUrl>(json);
        //StartCoroutine(GetText());
       // UserData.data = data;
        UserData.UserPass = true;
    }
    IEnumerator GetText()
    {

        Directory.CreateDirectory(getPath()+"/Image");
        Directory.CreateDirectory(getPath() + "/Image/Story1");
        Directory.CreateDirectory(getPath() + "/Image/Story2");
        Directory.CreateDirectory(getPath() + "/Image/Story3");
        Directory.CreateDirectory(getPath() + "/Image/Icon");
        Directory.CreateDirectory(getPath() + "/Image/PostImage");
        for (int i = 0; i < data.Data.Length; i++)
        {
            print(url + data.Data[i].ID);
            UnityWebRequest imgRequest = UnityWebRequestTexture.GetTexture(url+ data.Data[i].ID);

            yield return imgRequest.SendWebRequest();
            print(getPath() + "/" + data.Data[i].Name);
            print($"{i+1}/{data.Data.Length}");
            if (imgRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(imgRequest.error);
                break;
            }
            else
            {
                string name = Path.GetFileName(url);
                Texture2D tex = DownloadHandlerTexture.GetContent(imgRequest);
                print(tex.format);
                byte[] itemBGBytes = tex.EncodeToPNG();
                File.WriteAllBytes(getPath() + "/"+ data.Data[i].Name, itemBGBytes);

                
            }
        }
    }
    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath+ "/Test";
#elif UNITY_ANDROID
        return Application.persistentDataPath ;
#elif UNITY_IPHONE
        return Application.persistentDataPath;
#else
        return  "/CSV/" + UserData.Story + "_" + UserData.Solution+ "_" + UserData.UserID + ".csv";
#endif
        Debug.Log("get path leido");
    }
}

[System.Serializable]
public class ImageUrl
{
    public ImageData[] Data;
    public bool Check;
}
[System.Serializable]
public class ImageData
{
    public string Name;
    public string ID;
}
