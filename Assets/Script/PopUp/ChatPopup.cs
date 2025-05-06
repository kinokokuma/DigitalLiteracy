using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using System.Text.RegularExpressions;

public class ChatPopup : BasePopUp
{
    private const int chatSizeOffset = 660; //285+70
    private ChatData data;
    private int chatIndex;
    private string[] emoji = { "<sprite=0>", "<sprite=1>", "<sprite=2>", "<sprite=3>", "<sprite=4>", "<sprite=5>" };

    [SerializeField]
    private Button allChatButton;
    [SerializeField]
    private GameObject allChatObject;
    [SerializeField]
    private TMP_Text userInputText;
    [SerializeField]
    private TMP_Text headerName;
    [SerializeField]
    private Image[] headerIcon;
    [SerializeField]
    private Image[] headerIconMask;
    [SerializeField]
    private ChatObjectBase chatobject;
    [SerializeField]
    private RectTransform chatViewPoint;
    [SerializeField]
    private RectTransform chatParent;
    [SerializeField]
    private Transform imageChoiceParent;
    [SerializeField]
    private Transform QuestionObject;
    [SerializeField]
    private ChatChoice imageChoice;
    [SerializeField]
    private ScrollRect chatParentScrollRect;
    [SerializeField]
    private GameObject reloadObj;
    [SerializeField]
    private RectTransform bottomChat;
    [SerializeField]
    public List<ChatObjectBase> chatObject;
    public GameObject allChatGuildlind;
    private bool isReload;
    public GameObject video;
    private List<ChatChoice> choiceList = new List<ChatChoice>();
    private float timeToShowQuestion;
    private bool isFirstTime = true;
    private int oldIndex = 0;
    public GameObject textGuild;
    public GameObject imageGuild;
    public GameObject blockText;
    public Image header;
    public Image headerImage;
    public AspectRatioFitter headerFitter;

    [SerializeField]
    private GameObject mockReload;
    [SerializeField]
    private GameObject home;
    public void Start()
    {
        allChatButton.interactable = false;
           chatObject = new List<ChatObjectBase>();
        allChatButton.onClick.AddListener(ShowChatList);
        allChatGuildlind.SetActive(false);
        //ReadData();
    }

    private void ShowChatList()
    {
        TimeRecord.Instance.SaveRecord(this.data.ID + "-" + manager.CurrentJsonName, $"เปิดแชททั้งหมด", manager.timeToClickChat, false);
        manager.ShowAllChat();
        allChatButton.interactable = false;
        allChatGuildlind.SetActive(false);
    }



    public IEnumerator ShowChat(ChatData data,bool muteSound,bool showLineHome)
    {
        if (showLineHome)
        {
            home.SetActive(true);
            yield return new WaitForSeconds(3);
            home.SetActive(false);
        }

        oldIndex = 0;
        chatIndex = 0;
        this.data = data;
        int contextCharecterIndex = 0;
        ID = data.ID;
        if (isFirstTime)
        {

            headerName.text = data.ChatName;
            print(data.ChatName);
            if (data.Icon.Length >=2)
            {
                headerIcon[0].sprite = ImageManager.Instance.LoadImage(data.Icon[0]);
                headerIcon[1].sprite = ImageManager.Instance.LoadImage(data.Icon[1]);
            }
            else if(data.Icon != null)
            {
                headerIcon[1].sprite = ImageManager.Instance.LoadImage(data.Icon[0]);
                headerIconMask[0].gameObject.SetActive(false);
            }

            if(data.Header != null)
            {
                headerImage.sprite = ImageManager.Instance.LoadImage(data.Header);
                header.gameObject.SetActive(true);
                headerFitter.aspectRatio = (float)headerImage.sprite.texture.width / (float)headerImage.sprite.texture.height;
            }

            isFirstTime = false;
        }

        bool haveQuestion =false;

        while (chatIndex < data.DataDetail.Length)
        {
            if (oldIndex != chatIndex || oldIndex == 0)
            {
                if (data.DataDetail[chatIndex].ChoiceImage != null)
                {
                    if (data.DataDetail[chatIndex].ChoiceImage.Length > 0)
                    {
                        for (int i = 0; i < data.DataDetail[chatIndex].ChoiceImage.Length; i++)
                        {
                            timeToShowQuestion = Time.time;
                            ChatChoice choice = Instantiate(imageChoice, imageChoiceParent);
                            choice.gameObject.SetActive(true);
                            if(data.DataDetail[chatIndex].ChoiceImage[i].CanClick == false)
                            {
                                imageGuild.SetActive(true);
                            }

                            choice.InitializedImage(i, ChoiceType.Image, data.DataDetail[chatIndex].ChoiceImage[i]);
                            choice.Button.onClick.AddListener(() => StartCoroutine(OnClickImageChoice(choice.DataImage)));
                            choiceList.Add(choice);
                        }
                        QuestionObject.gameObject.SetActive(true);
                        chatViewPoint.sizeDelta = new Vector2(chatViewPoint.sizeDelta.x, chatViewPoint.sizeDelta.y - chatSizeOffset);
                        haveQuestion = true;
                    }
                }
                else if (data.DataDetail[chatIndex].Choice != null)
                {
                    if (data.DataDetail[chatIndex].Choice.Length > 0)
                    {
                        //CreateLike();
                        for (int i = 0; i < data.DataDetail[chatIndex].Choice.Length; i++)
                        {
                            timeToShowQuestion = Time.time;
                            textGuild.SetActive(true);
                            ChatChoice choice = Instantiate(imageChoice, imageChoiceParent);
                            choice.gameObject.SetActive(true);
                            choice.InitializedText(i, ChoiceType.String, data.DataDetail[chatIndex].Choice[i]);
                            choice.Button.onClick.AddListener(() => StartCoroutine(OnClickTextChoice(choice.DataText, data.DataDetail[data.DataDetail.Length - 1])));

                            choiceList.Add(choice);
                        }
                        QuestionObject.gameObject.SetActive(true);
                        chatViewPoint.sizeDelta = new Vector2(chatViewPoint.sizeDelta.x, chatViewPoint.sizeDelta.y - chatSizeOffset);
                        haveQuestion = true;
                    }
                }
                else
                {
                    data.DataDetail[chatIndex].Content = data.DataDetail[chatIndex].Content.Replace("ป้าก้อย", $"{UserData.UserSex}{UserData.UserName}");
                    data.DataDetail[chatIndex].Content = data.DataDetail[chatIndex].Content.Replace("{type}", UserData.UserSex);
                    if (data.DataDetail[chatIndex].OnwerName == "my" && data.DataDetail[chatIndex].DelayTime !=0)
                    {
                        if(data.DataDetail[chatIndex].DelayTime == 1)
                        {
                            data.DataDetail[chatIndex].DelayTime = 3;
                        }
                        if(UserData.UserSex == "ลุง")
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

                            for (contextCharecterIndex = 0; contextCharecterIndex < inputString.Length; contextCharecterIndex++)
                            {
                                if (inputString[contextCharecterIndex] == '๑')
                                {
                                    userInputText.text += "<sprite=0>";
                                }
                                else if(inputString[contextCharecterIndex] == '๒')
                                {
                                    userInputText.text += "<sprite=1>";
                                }
                                else if (inputString[contextCharecterIndex] == '๓')
                                {
                                    userInputText.text += "<sprite=2>";
                                }
                                else if (inputString[contextCharecterIndex] == '๔')
                                {
                                    userInputText.text += "<sprite=3>";
                                }
                                else if (inputString[contextCharecterIndex] == '๕')
                                {
                                    userInputText.text += "<sprite=4>";
                                }
                                else if (inputString[contextCharecterIndex] == '๖')
                                {
                                    userInputText.text += "<sprite=5>";
                                }
                                else if (inputString[contextCharecterIndex] == '๗')
                                {
                                    userInputText.text += "<sprite=6>";
                                }
                                else
                                {
                                    userInputText.text += inputString[contextCharecterIndex];
                                }
                                yield return new WaitForSeconds(0.05f);
                            }
                            

                            ChatObjectBase chat = Instantiate(chatobject);
                            chat.gameObject.SetActive(true);
                            chat.isSingle = data.Icon.Length >= 2 ? false : true;
                            chat.Initialized(data.DataDetail[chatIndex], this, manager, muteSound);
                            chatObject.Add(chat);
                            chat.gameObject.transform.SetParent(chatParent);
                            oldIndex = chatIndex;
                            userInputText.text = string.Empty;
                            //Reload();
                        }
                    }
                    else
                    {
                        if (data.DataDetail[chatIndex].OnwerName == "ซี" && UserData.UserSex == "หญิง")
                        {
                            data.DataDetail[chatIndex].Content = data.DataDetail[chatIndex].Content.Replace("ค่ะ", "ครับ");
                            data.DataDetail[chatIndex].Content = data.DataDetail[chatIndex].Content.Replace("คะ", "ครับ");
                        }
                        ChatObjectBase chat = Instantiate(chatobject, chatParent);
                        chat.gameObject.SetActive(true);
                        chat.isSingle = data.Icon.Length >= 2 ? false : true;
                        chat.Initialized(data.DataDetail[chatIndex], this, manager, muteSound);
                        chatObject.Add(chat);
                        oldIndex = chatIndex;
                        oldIndex = chatIndex;
                        //Reload();
                    }

                }
                StartCoroutine(UpdateLayoutGroup());
                //Reload();





            }
            if (haveQuestion) break;

            yield return new WaitForEndOfFrame();
            LayoutRebuilder.ForceRebuildLayoutImmediate(chatParent);
            yield return new WaitForEndOfFrame();
            if (mockReload!=null)
            {
                Destroy(mockReload);
            }
            mockReload = Instantiate(reloadObj, chatParent);
            yield return new WaitForEndOfFrame();
            LayoutRebuilder.ForceRebuildLayoutImmediate(chatParent);

            yield return new WaitForSeconds(data.DataDetail[chatIndex].DelayTime == 1?3: data.DataDetail[chatIndex].DelayTime);
            chatIndex++;

        }

        yield return new WaitForSeconds(5);

        if (!haveQuestion && data.DataDetail[chatIndex-1].ChatType == "Normal")
        {
            manager.NextFileName = manager.GetSpPath(data.DataDetail[data.DataDetail.Length - 1].FileName);
            if (data.DataDetail[chatIndex - 1].LinkType == "chat")
            {
                ChatData newData = manager.ReadChatData($"Feed/{UserData.Story}/{data.DataDetail[data.DataDetail.Length - 1].FileName}");
                print("check ID : "+ID + " " + newData.ID);
               /* if (newData.ID != ID)
                {
                    manager.timeToClickChat = Time.time;
                    allChatButton.interactable = true;
                    allChatGuildlind.SetActive(true);
                    manager.NextChatID = data.DataDetail[data.DataDetail.Length - 1].ID;
                    if (!muteSound)
                    {
                        SoundManager.Instance.PlaySound(SoundID.newChat);
                    }
                }
                else*/
                {
                    print(data.DataDetail[data.DataDetail.Length - 1].FileName);
                    manager.OpenChat(data.DataDetail[data.DataDetail.Length - 1].FileName);
                }
            }
            else if (!haveQuestion && data.DataDetail[chatIndex - 1].LinkType == "SP1")
            {
                manager.SP1Button.SetActive(true);
                Button b = manager.SP1Button.GetComponent<Button>();
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(() => manager.OnclickOgpage(manager.SP1Button, (data.DataDetail[data.DataDetail.Length - 1].FileName)));
            }
            else if (!haveQuestion && data.DataDetail[chatIndex - 1].LinkType == "dialog")
            {
                manager.gopageButton.SetActive(true);
                Button b = manager.gopageButton.GetComponent<Button>();
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(() => manager.OpenDialog((data.DataDetail[data.DataDetail.Length - 1].FileName)));
                b.onClick.AddListener(() => b.gameObject.SetActive(false));
            }
            else if(data.DataDetail[chatIndex - 1].LinkType != "" || data.DataDetail[chatIndex - 1].LinkType != null)
            {
                manager.gopageButton.SetActive(true);
                Button b = manager.gopageButton.GetComponent<Button>();
                b.onClick.RemoveAllListeners();
                b.onClick.AddListener(() => manager.OnclickOgpage(manager.gopageButton, (data.DataDetail[data.DataDetail.Length - 1].FileName)));
            }

            if(data.DataDetail[chatIndex - 1].ID == "block")
            {
                blockText.SetActive(true);
                bottomChat.gameObject.SetActive(false);
            }

        }
    }


    public void CreateLike()
    {
        ChatObjectBase chat = Instantiate(chatobject);
        chat.gameObject.SetActive(true);
        chat.isSingle = data.Icon.Length >= 2 ? false : true;
        ChatDataDetail dataDetail = new ChatDataDetail();
        chatObject.Add(chat);
        dataDetail.OnwerName = "my";
        dataDetail.Icon = string.Empty;
        dataDetail.Content = "";
        dataDetail.PostImage = "Image/Like";


        chat.Initialized(dataDetail, this, manager);
        chatObject.Add(chat);
        chat.gameObject.transform.SetParent(chatParent);
    }

    
    int countLoad = 0;

    private void LateUpdate()
    {

        if (chatParent.sizeDelta.y <= 1134)
        {
            chatParent.pivot = new Vector2(chatParent.pivot.x, 1);
        }
        else
        {
            chatParent.pivot = new Vector2(chatParent.pivot.x, 0);
        }
        /* if (isReload)
         {
             chatParent.anchoredPosition = new Vector2(chatParent.anchoredPosition.x, chatParent.sizeDelta.y);
             countLoad++;
             //if (countLoad > 4)
             {
                 isReload = false;
                 countLoad = 0;
             }
         }*/
        if (manager != null)
        {
            /*if (manager.allChat.active)
            {
                bottomChat.sizeDelta = new Vector2(635, bottomChat.sizeDelta.y);
            }
            else
            {
                bottomChat.sizeDelta = new Vector2(1024, bottomChat.sizeDelta.y);
            }*/
        }
    }

    private IEnumerator OnClickTextChoice(ChoiceText choiceText, ChatDataDetail data)
    {
        HintChoice();
        ChatObjectBase chat = Instantiate(chatobject, chatParent);
        chat.gameObject.SetActive(true);
        chat.isSingle = data.Icon.Length >= 2 ? false : true;
        ChatDataDetail dataDetail = new ChatDataDetail();
        chatObject.Add(chat);
        dataDetail.OnwerName = "my";
        dataDetail.Icon = string.Empty;
        dataDetail.Content = data.Content.Replace("{0}", choiceText.Path);
        if (UserData.UserSex == "ลุง")
        {
            dataDetail.Content = dataDetail.Content.Replace("ค่ะ", "ครับ");
            dataDetail.Content = dataDetail.Content.Replace("คะ", "ครับ");
        }
        dataDetail.PostImage = string.Empty;
        dataDetail.LinkType = choiceText.LinkType;
        dataDetail.FileName = choiceText.FileName;

        TimeRecord.Instance.SaveRecord(this.data.ID+"-"+manager.CurrentJsonName, choiceText.Path, timeToShowQuestion, choiceText.IsSignificant);

        chat.Initialized(dataDetail, this, manager);
        QuestionObject.gameObject.SetActive(false);


        chatViewPoint.sizeDelta = new Vector2(chatViewPoint.sizeDelta.x, chatViewPoint.sizeDelta.y + chatSizeOffset);

        if ((int)chatParent.sizeDelta.y >= (int)chatParent.anchoredPosition.y)
        {
            isReload = true;
        }
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(chatParent);

        manager.NextFileName = dataDetail.FileName;
        if (choiceText.LinkType == "chat")
        {
            ChatData newData = manager.ReadChatData($"Feed/{UserData.Story}/{dataDetail.FileName}");
            print("check ID : " + ID + " " + newData.ID);
           /* if (newData.ID != ID)
            {
                manager.timeToClickChat = Time.time;
                allChatButton.interactable = true;
                allChatGuildlind.SetActive(true);
                print(data.ID);
                manager.NextChatID = data.ID;
                SoundManager.Instance.PlaySound(SoundID.newChat);
                
            }
            else*/
            {
                print(dataDetail.FileName);
                yield return new WaitForSeconds(3);
                manager.OpenChat(dataDetail.FileName);
            }
        }
        else if(choiceText.LinkType != null)
        {   
            manager.CreatePopup(choiceText.FileName);
        }
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(chatParent);
        //StartCoroutine(UpdateLayoutGroup(reloadObj));
    }

    private void HintChoice()
    {
        imageGuild.SetActive(false);
        textGuild.SetActive(false);
        foreach (var choice in choiceList)
        {
            choice.gameObject.SetActive(false);
        }
    }

    private IEnumerator OnClickImageChoice(ChoiceImage choiceImage)
    {
        HintChoice();
        ChatObjectBase chat = Instantiate(chatobject, chatParent);
        chat.gameObject.SetActive(true);
        chat.isSingle = data.Icon.Length >= 2 ? false : true;
        ChatDataDetail dataDetail = new ChatDataDetail();
        chatObject.Add(chat);
        dataDetail.OnwerName = "my";
        dataDetail.Icon = string.Empty;
        dataDetail.Content = string.Empty;
        dataDetail.PostImage = choiceImage.Path;
        dataDetail.LinkType = choiceImage.LinkType;
        dataDetail.FileName = choiceImage.FileName;
        chat.Initialized(dataDetail, this, manager);
        QuestionObject.gameObject.SetActive(false);

        TimeRecord.Instance.SaveRecord(this.data.ID + "-" + manager.CurrentJsonName, Path.GetFileName(choiceImage.Path), timeToShowQuestion, choiceImage.IsSignificant);

        chatIndex = 0;

        chatViewPoint.sizeDelta = new Vector2(chatViewPoint.sizeDelta.x, chatViewPoint.sizeDelta.y + chatSizeOffset);

        if ((int)chatParent.sizeDelta.y >= (int)chatParent.anchoredPosition.y)
        {
            isReload = true;
        }
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(chatParent);
        yield return new WaitForSeconds(3);
        manager.OpenChat(choiceImage.FileName);
        manager.NextFileName = choiceImage.FileName;
        
    }

    public void ClearChat()
    {
        print("Button");
        if (chatObject.Count > 0)
        {
            print("Button");
            foreach (var chat in chatObject)
            {
                Destroy(chat.gameObject);
            }
            chatObject.Clear();
        }
    }
}
