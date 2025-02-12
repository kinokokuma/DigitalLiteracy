using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GoToChatButton : MonoBehaviour
{
    public string ID;
    public Button button;
    public Image image;
    public TMP_Text text;
    public ChatData data;
    public RectTransform t;
    PopUpManager manager;
    void Start()
    {

    }

    // Update is called once per frame
    public void Initialized(ChatData data, PopUpManager manager)
    {
        this.data = data;
        this.manager = manager;
        ID = data.ID;
        text.text = data.ChatName;
        if (data.Icon.Length == 1)
        {
            image.sprite = ImageManager.Instance.LoadImage(data.Icon[0]);
        }
        else
        {
           // image.sprite = ImageManager.Instance.LoadImage($"Image/{UserData.Story}/GP");
        }
        button.onClick.AddListener(click);
    }

    public void click()
    {
        TimeRecord.Instance.SaveRecord(ID, $"เปิดแชท{ID}",manager.timeToClickChat);
        manager.HindAllChat();
        print(manager.NextFileName);
        manager.OpenChat(manager.NextFileName);
    }
}
