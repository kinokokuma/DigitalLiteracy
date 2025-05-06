using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Analytics;
using System.IO;
public class DialogPopup : BasePopUp
{
    [SerializeField]
    private Button nextChat;

    [SerializeField]
    private Image BG;
    [SerializeField]
    private GameObject leftChat;
    [SerializeField]
    private TMP_Text leftChatName;
    [SerializeField]
    private TMP_Text leftChatText;
    [SerializeField]
    private Image leftChatImage;

    [SerializeField]
    private GameObject rightChat;
    [SerializeField]
    private TMP_Text rightChatName;
    [SerializeField]
    private TMP_Text rightChatText;
    [SerializeField]
    private Image rightChatImage;

    [SerializeField]
    private GameObject midChat;
    [SerializeField]
    private TMP_Text midChatText;

    private bool isReload;
    private List<ChatChoice> choiceList = new List<ChatChoice>();
    private float timeToShowQuestion;
    private int oldIndex = 0;
    private DialogData data;
    private int chatIndex;
    private string[] emoji = { "<sprite=0>", "<sprite=1>", "<sprite=2>", "<sprite=3>", "<sprite=4>", "<sprite=5>" };

    [SerializeField]
    private Transform choiceParent;
    [SerializeField]
    private Transform QuestionObject;
    [SerializeField]
    private ChatChoice imageChoice;
    [SerializeField]
    private GameObject reload;

    [SerializeField]
    private TMP_Text allDialogText;
    private string allDialog;
    void Start()
    {
       /* rightChat.SetActive(false);
        leftChat.SetActive(false);
        midChat.SetActive(false);
        leftChatImage.gameObject.SetActive(false);
        rightChatImage.gameObject.SetActive(false);*/
    }

    public void ShowDialog(DialogData data)
    {
        oldIndex = 0;
        chatIndex = 0;
        this.data = data;
        if (data.BG != string.Empty)
        {
            print($"Image/{UserData.Story}/{data.BG}");
            var poseTexture = Resources.Load<Texture2D>($"Image/{UserData.Story}/{data.BG}");
            BG.sprite = Sprite.Create(poseTexture, new Rect(0.0f, 0.0f, poseTexture.width, poseTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
        int contextCharecterIndex = 0;
        ID = data.ID;
        leftChatImage.gameObject.SetActive(false);
        rightChatImage.gameObject.SetActive(false);
        OnNextChat();
        bool haveQuestion = false;
    }
      
    IEnumerator Reload()
    {
        reload.SetActive(false);
        yield return new WaitForFixedUpdate();//WaitForSeconds(0.1f);
        reload.SetActive(true);
    }
    public void OnNextChat()
    {
        bool haveQuestion = false;
        nextChat.gameObject.SetActive(true);
        if (data.DataDetail.Length > chatIndex)
        {
            if(data.DataDetail.Length-1 == chatIndex)
            {
                //nextChat.gameObject.SetActive(false);
            }
            if (data.DataDetail[chatIndex].Choice != null)
            {

                if (data.DataDetail[chatIndex].Choice.Length > 0)
                {
                    //CreateLike();
                    //
                    QuestionObject.gameObject.SetActive(true);
                    for (int i = 0; i < data.DataDetail[chatIndex].Choice.Length; i++)
                    {
                        timeToShowQuestion = Time.time;
                        ChatChoice choice = Instantiate(imageChoice, choiceParent);
                       // choice.gameObject.SetActive(true);
                        choice.InitializedText(i, ChoiceType.String, data.DataDetail[chatIndex].Choice[i]);
                        choice.Button.onClick.AddListener(() => StartCoroutine(OnClickTextChoice(choice.DataText, data.DataDetail[data.DataDetail.Length - 1])));
                        choiceList.Add(choice);
                        choice.gameObject.SetActive(true);
                    }
                    haveQuestion = true;
                    StartCoroutine(Reload());
                    nextChat.gameObject.SetActive(false);
                }
            }
            else
            {
                data.DataDetail[chatIndex].Content = data.DataDetail[chatIndex].Content.Replace("ป้าก้อย", $"{UserData.UserSex}{UserData.UserName}");
                data.DataDetail[chatIndex].Content = data.DataDetail[chatIndex].Content.Replace("{type}", UserData.UserSex);
                if (data.DataDetail[chatIndex].ChatSide == "right")
                {
                    if (data.DataDetail[chatIndex].DelayTime == 1)
                    {
                        data.DataDetail[chatIndex].DelayTime = 3;
                    }
                    if (UserData.UserSex == "ลุง")
                    {
                        data.DataDetail[chatIndex].Content = data.DataDetail[chatIndex].Content.Replace("ป้า", "ลุง");
                        data.DataDetail[chatIndex].Content = data.DataDetail[chatIndex].Content.Replace("ค่ะ", "ครับ");
                        data.DataDetail[chatIndex].Content = data.DataDetail[chatIndex].Content.Replace("คะ", "ครับ");
                    }

                    if (oldIndex != chatIndex || oldIndex == 0)
                    {
                        string inputString = data.DataDetail[chatIndex].Content.Replace("<sprite=0>", "๑");
                        inputString = inputString.Replace("<sprite=1>", "๒");
                        inputString = inputString.Replace("<sprite=2>", "๓");
                        inputString = inputString.Replace("<sprite=3>", "๔");
                        inputString = inputString.Replace("<sprite=4>", "๕");
                        inputString = inputString.Replace("<sprite=5>", "๖");
                        inputString = inputString.Replace("<sprite=6>", "๗");
                    }
                }
                else
                {
                    if (data.DataDetail[chatIndex].OnwerName == "ซี" && UserData.UserSex == "หญิง")
                    {
                        data.DataDetail[chatIndex].Content = data.DataDetail[chatIndex].Content.Replace("ค่ะ", "ครับ");
                        data.DataDetail[chatIndex].Content = data.DataDetail[chatIndex].Content.Replace("คะ", "ครับ");
                    }
                    oldIndex = chatIndex;
                    //Reload();
                }
                string front = data.DataDetail[chatIndex].ChatSide == "right" ? "คุณ " : data.DataDetail[chatIndex].ChatSide == "left" ? data.DataDetail[chatIndex].OnwerName : "คำบรรยาย";
                allDialogText.text += front + $" : {data.DataDetail[chatIndex].Content}\n\n";
            }
            //Reload();
            leftChatImage.gameObject.SetActive(false);
            rightChatImage.gameObject.SetActive(false);
            if (data.DataDetail[chatIndex].DialogImage != null)
            {
                foreach (var imagedata in data.DataDetail[chatIndex].DialogImage)
                {
                    if (imagedata.ImageSide == "left")
                    {
                        leftChatImage.gameObject.SetActive(true);
                        leftChatImage.sprite = ImageManager.Instance.LoadImage(imagedata.Image);
                        if (imagedata.Active)
                        {
                            leftChatImage.color = new Color(1, 1, 1);
                        }
                        else
                        {
                            leftChatImage.color = new Color(0.4f, 0.4f, 0.4f);
                        }
                    }
                    else
                    {
                        rightChatImage.gameObject.SetActive(true);
                        rightChatImage.sprite = ImageManager.Instance.LoadImage(imagedata.Image);
                        if (imagedata.Active)
                        {
                            rightChatImage.color = new Color(1, 1, 1);
                        }
                        else
                        {
                            rightChatImage.color = new Color(0.4f, 0.4f, 0.4f);
                        }
                    }
                }
            }

            leftChat.SetActive(false);
            rightChat.SetActive(false);
            midChat.SetActive(false);
            if (data.DataDetail[chatIndex].ChatSide == "left")
            {
                leftChat.SetActive(true);
                leftChatName.text = data.DataDetail[chatIndex].OnwerName;
                leftChatText.text = data.DataDetail[chatIndex].Content;
            }
            else if (data.DataDetail[chatIndex].ChatSide == "right")
            {
                rightChat.SetActive(true);

                rightChatName.text = data.DataDetail[chatIndex].OnwerName=="ป้าก้อย"?"คุณ": data.DataDetail[chatIndex].OnwerName;
                rightChatText.text = data.DataDetail[chatIndex].Content;
            }
            else
            {
                midChat.SetActive(true);
                midChatText.text = data.DataDetail[chatIndex].Content;
            }

            chatIndex++;
        }
        else {
            print(data.DataDetail[chatIndex - 1].ChatType);
            if (data.DataDetail[chatIndex - 1].ChatType == "Normal")
            {
                print("x2");
                manager.NextFileName = manager.GetSpPath(data.DataDetail[data.DataDetail.Length - 1].FileName);
                if (data.DataDetail[chatIndex - 1].LinkType == "chat")
                {
                    ChatData newData = manager.ReadChatData($"Feed/{UserData.Story}/{data.DataDetail[data.DataDetail.Length - 1].FileName}");
                    print("check ID : " + ID + " " + newData.ID);
                    {
                        print(data.DataDetail[data.DataDetail.Length - 1].FileName);
                        manager.OpenChat(data.DataDetail[data.DataDetail.Length - 1].FileName,true);
                    }
                    gameObject.SetActive(false);
                }
                else if (data.DataDetail[chatIndex - 1].LinkType == "dialog")
                {
                    manager.OpenDialog(data.DataDetail[data.DataDetail.Length - 1].FileName);
                }
                else if (data.DataDetail[chatIndex - 1].LinkType != "" || data.DataDetail[chatIndex - 1].LinkType != null)
                {
                    manager.CreatePopup(data.DataDetail[data.DataDetail.Length - 1].FileName);
                    // manager.gopageButton.SetActive(true);
                    // Button b = manager.gopageButton.GetComponent<Button>();
                    //b.onClick.RemoveAllListeners();
                    // b.onClick.AddListener(() => manager.OnclickOgpage(manager.gopageButton, data.DataDetail[data.DataDetail.Length - 1].FileName));
                }

            }
        }
    }

    private IEnumerator OnClickTextChoice(ChoiceText choiceText, DialogDataDetail data)
    {
        print("yyy");
        HintChoice();
        QuestionObject.gameObject.SetActive(false);

        manager.NextFileName = choiceText.FileName;
        if (choiceText.LinkType == "chat")
        {
            print(choiceText.FileName);
            ChatData newData = manager.ReadChatData($"Feed/{UserData.Story}/{choiceText.FileName}");
            print("check ID : " + ID + " " + newData.ID);
 
            yield return new WaitForSeconds(3);
            manager.OpenChat(choiceText.FileName, true);
            gameObject.SetActive(false);
            
        }
        else if (choiceText.LinkType == "dialog")
        {
            manager.OpenDialog(choiceText.FileName);
        }
        else if (choiceText.LinkType != null)
        {
            manager.CreatePopup(choiceText.FileName);
            gameObject.SetActive(false);
        }
    }
    private void HintChoice()
    {
        choiceParent.gameObject.SetActive(false);
        foreach (var choice in choiceList)
        {
            choice.gameObject.SetActive(false);
        }
    }
}
