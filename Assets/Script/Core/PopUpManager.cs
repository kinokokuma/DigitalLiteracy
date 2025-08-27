using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum QuestionPhase
{
    Is_Fake,
    Level_Of_Confident,
    Have_seen,

}

public class PopUpManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject allChat;
    [SerializeField]
    private FeedData data;
    [SerializeField]
    private PostPopup popupPrefab;
    [SerializeField]
    private Transform feedParent;
    [SerializeField]
    private Transform popupParent;
    [SerializeField]
    private Transform chatParent;
    [SerializeField]
    private ScrollRect scrollRect;
    [SerializeField]
    private ChatPopup chatPopup;
    [SerializeField]
    private VerticalLayoutGroup layoutGroup;
    [SerializeField]
    private QuestPopUp question;
    private QuestionPhase phase;
    private PostData currentPostPopupData;
    [SerializeField]
    private DialogPopup dialogPopup;

    [SerializeField]
    private Dictionary<string, ChatPopup> chatPopUpDic;
    [SerializeField]
    private Dictionary<string, BasePopUp> popUpDic;

    [SerializeField]
    private Transform goToChatParent;
    public GoToChatButton chatButton;
    public List<GoToChatButton> allChatButtonList;
    public DialogPopup Dialog;
    [SerializeField]
   // private List<BasePopUp> allPopUpList;
    public QuestionPhase Phase => phase;
    public  PostData CurrentPostPopupData => currentPostPopupData;
    public ChatDataDetail currentChatData;
    public string NextFileName;
    public string NextChatID;
    public string CurrentJsonName;
    public RectTransform GotoChatGuildLine;
    [SerializeField]
    private RectTransform bottomChat;
    public GameObject startObj;
    public float timeToClickChat;
    public GameObject gopageButton;
    public GameObject SP1Button, SP2Button;
    public string OldChatname;
    public void SetPhase(QuestionPhase phase)
    {
        this.phase = phase;
    }

    public void Awake()
    {
        TimeRecord.Instance.CreatePlayerCsv();
        chatPopUpDic = new Dictionary<string, ChatPopup>();
        popUpDic = new Dictionary<string, BasePopUp>();
       // data = new FeedData();
       // data = JsonUtility.FromJson<FeedData>(ReadFile("Feed/Class1").ToString());
        dialogPopup.SetManager(this);
        timeToClickChat = Time.time;
        StartCoroutine(StartChatStory());
    }

    IEnumerator StartChatStory()
    {
        if (UserData.Story == "Story1")
        {
           // OpenChat("storyc", true);
        }
        else if (UserData.Story == "Story3")
        {
          /*  OpenChat("story3-0-2", true, true);
            OpenChat("story3-0-3", true, true);*/
        }
        while (NextFileName != "")
        {

        }
        yield return new WaitForEndOfFrame();

        OpenDialog($"{UserData.Story.ToLower()}-1");
        // OpenDialog($"story2-5");
       // OpenChat("story1-8-2");
       //OpenChat("story1-8-2");
        // OpenChat($"Route1/story1-10-A-1");
        startObj.SetActive(false);
    }

    public void OnclickOgpage(GameObject obj, string data)
    {
        obj.SetActive(false);
        CreatePopup(data);
    }

    public TextAsset ReadFile(string fileName)
    {
        return Resources.Load<TextAsset>("Feed/Class1");
    }

    private void Start()
    {
        foreach (var postData in data.PostData)
        {
            var popup = Instantiate(popupPrefab, feedParent);
            popup.gameObject.SetActive(true);
            popup.Initialized(postData, this);
        }
    }

    public void SetCurrentData(PostData data)
    {
        currentPostPopupData = data;
    }
    public void Update()
    {
        if (Dialog.gameObject.active)
        {
           // SoundManager.Instance.BGMsource.Play();
        }
        else
        {
          //  SoundManager.Instance.BGMsource.Pause();
        }

        if (currentPostPopupData != null)
        {
            if (currentPostPopupData.IsTask)
            {
                scrollRect.enabled = false;
            }
            else
            {
                scrollRect.enabled = true;
            }
        }
    }

    public string GetSpPath(string path)
    {
        if (path == "story2-16")
        {
            if (!UserData.S2Pass)
            {
                path = "story2-16-1";
            }
        }
        return path;
    }

    public void OpenChat(string path, bool showLineHome = false, bool muteSound = false, bool isOld = false)
    {
        path = GetSpPath(path);
        dialogPopup.gameObject.SetActive(false);
        ChatData newData = ReadChatData($"Feed/{UserData.Story}/{path}");
        print(path);
        print(newData.ID);
        string id = newData.ID;
        TimeRecord.Instance.LogCheck(path);
        if (chatPopUpDic.ContainsKey(id))
        {
            foreach(BasePopUp popUp in chatPopUpDic.Values)
            {
                popUp.gameObject.SetActive(false);
            }

            chatPopUpDic[id].gameObject.SetActive(true);
            StopCoroutine(chatPopUpDic[id].ShowChat(newData, muteSound, showLineHome));
            StartCoroutine(chatPopUpDic[id].ShowChat(newData, muteSound, showLineHome));

        }
        else
        {
            foreach (BasePopUp popUp in chatPopUpDic.Values)
            {
                popUp.gameObject.SetActive(false);
            }

            ChatPopup popup = Instantiate(chatPopup, chatParent);
            popup.SetManager(this);
            popup.gameObject.SetActive(true);
            chatPopUpDic[id] = popup;
            popUpDic[id] = popup;
            StartCoroutine(popup.ShowChat(newData,muteSound, showLineHome));

            GoToChatButton butt = Instantiate(chatButton, goToChatParent);
            butt.gameObject.SetActive(true);
            butt.Initialized(newData, this);
            allChatButtonList.Add(butt);
        }

        CurrentJsonName = path;
    }

    public void OpenDialog(string path)
    {
        path = GetSpPath(path);
        print($"Feed/{UserData.Story}/{path}");
        DialogData newData = ReadDialogData($"Feed/{UserData.Story}/{path}");
        string id = newData.ID;
        TimeRecord.Instance.LogCheck(path);
        dialogPopup.gameObject.SetActive(true);
        dialogPopup.ShowDialog(newData);
        CurrentJsonName = path;
    }

    public void ShowAllChat( )
    {
        timeToClickChat = Time.time;
        ChatData data = ReadChatData($"Feed/{UserData.Story}/{NextFileName}");
        allChat.SetActive(true);

        foreach (var button in allChatButtonList)
        {
            if(button.ID == data.ID)
            {
                StartCoroutine(ShowAllChatGuide(button));

            }
            else
            {
                button.button.interactable = false;
            }


        }

    }

    private IEnumerator ShowAllChatGuide(GoToChatButton button)
    {
        yield return new WaitForEndOfFrame();

        button.button.interactable = true;
        GotoChatGuildLine.gameObject.SetActive(true);
        GotoChatGuildLine.position = new Vector2(GotoChatGuildLine.position.x, button.t.position.y);
    }

    public void HindAllChat( )
    {
        allChat.SetActive(false);
        GotoChatGuildLine.gameObject.SetActive(false);
    }

    public ChatData ReadChatData(string path)
    {
        ChatData data = new ChatData();
        print(path);
        var jsonTextFile = Resources.Load<TextAsset>(path);

        return data = JsonUtility.FromJson<ChatData>(jsonTextFile.ToString());

        //StartCoroutine(ShowChat());

    }

    public DialogData ReadDialogData(string path)
    {
        DialogData data = new DialogData();
        var jsonTextFile = Resources.Load<TextAsset>(path);

        return data = JsonUtility.FromJson<DialogData>(jsonTextFile.ToString());

        //StartCoroutine(ShowChat());

    }

    public void click()
    {
        /* if ()
             ChatPopup popup = Instantiate<chatPopup,>();*/
        OpenChat(currentPostPopupData.TaskType);
        //chatPopup.gameObject.SetActive(true);
        //chatPopup.ReadData(currentPostPopupData.TaskType);
        //chatPopup.Initialized(currentPostPopupData,this);
        //StartCoroutine(CountToStartQuestion());

        

        //currentPostPopupData.IsTask = false;
    }
    public void Confirm()
    {
        currentPostPopupData.IsTask = false;
        question.gameObject.SetActive(false);
        chatPopup.gameObject.SetActive(false);
    }

    private IEnumerator CountToStartQuestion()
    {
        yield return new WaitForSeconds(5);
        question.gameObject.SetActive(true);
        question.viewPoint.sizeDelta = new Vector2(1024, 1366) - new Vector2(0, question.select.sizeDelta.y + 20);
    }

    public IEnumerator UpdateLayoutGroup()
    {
        layoutGroup.enabled = false;
        yield return new WaitForEndOfFrame();
        layoutGroup.enabled = true;
    }

    public BasePopUp FindPopup(string id)
    {
        foreach(var popUp in PopUpData.Instance.allPopUpList)
        {
            if(id == popUp.ID)
            {
                return popUp;
            }
        }

        return null;
    }

    public void CreatePopup(string id)
    {
        TimeRecord.Instance.LogCheck(id);
        if (chatPopUpDic.ContainsKey(id))
        {
            foreach (BasePopUp popUp in popUpDic.Values)
            {
                popUp.gameObject.SetActive(false);
            }

            popUpDic[id].gameObject.SetActive(true);

        }
        else
        {
            print(id);
            BasePopUp popup = Instantiate(FindPopup(id), popupParent);
            popup.SetManager(this);
            popup.gameObject.SetActive(true);
            popUpDic[id] = popup;
        }
    }

}
[System.Serializable]
public class FeedData
{
    public PostData[] PostData;
}
[System.Serializable]
public class PostData
{
    public string Name = string.Empty;
    public string ID = string.Empty;
    public string Icon = string.Empty;
    public string Description = string.Empty;
    public string PostImage = string.Empty;
    public string Time = string.Empty;
    public int LikeCount;
    public int CommentCount;
    public int ShereCount;
    public bool IsTask;
    public string TaskType;
    public PostData[] CommentData;
}