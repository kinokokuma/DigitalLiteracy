using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using UnityEngine.Networking;

public class UserData : MonoBehaviour
{
    public static string UserID;
    public static string Story;
    public static string UserName;
    public static string UserSex;
    public static bool UserPass =false;
    public static bool S2Pass = false;
    //public static ImageUrl data;
    public static int Story1PostIndex;

    public Button next;
    //public TMP_InputField inputID;
    public TMP_Dropdown dropdownStory;
    public TMP_Dropdown dropdownSolution;

    public Button nextPage;
    public TMP_InputField inputName;
    public TMP_Dropdown dropdownSex;
    public TMP_Text type;

    public GameObject User;
    public TMP_Text des;

    public void Start()
    {
        next.onClick.AddListener(() => { User.SetActive(true); });
    }

    public void Update()
    {

       /* if (inputID.text == string.Empty)
        {
            next.interactable = false;
        }
        else
        {
            next.interactable = true;
        }*/
        UserID = "";//inputID.text;
       // Solution = dropdownSolution.captionText.text;
        Story = dropdownStory.captionText.text;
       // type.text = Solution.Split('_')[1];
        if (inputName.text == string.Empty)
        {
            nextPage.interactable = false;
        }
        else
        {
            nextPage.interactable = true;
        }
        UserName = inputName.text;
        UserSex = dropdownSex.captionText.text=="ชาย"?"ลุง":"ป้า";
        print(UserSex);
    }

    void extract(string zipPath)
    {
        using (FileStream zipFileToOpen = new FileStream(zipPath, FileMode.OpenOrCreate))
        {
            using (ZipArchive archive = new ZipArchive(zipFileToOpen, ZipArchiveMode.Create))
            {
                archive.CreateEntryFromFile(@"D:\Example\file1.pdf", "file1.pdf");
                archive.CreateEntryFromFile(@"D:\Example\file2.pdf", "file2.pdf");
            }
        }
        UserPass = true;
    }

    IEnumerator GetText(string file_name)
    {
        string url = "https://drive.google.com/file/d/1CBmLGUoZCs7L4AdeQKpOSx84Kwa3ssru/view?usp=drive_link";
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.Send();
            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {

                string savePath = string.Format("{0}/{1}.zip", Application.persistentDataPath, file_name);
                System.IO.File.WriteAllText(savePath, www.downloadHandler.text);
            }
        }
    }
}
