using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class ChatObjectBase : MonoBehaviour
{
    protected ChatDataDetail data;
    protected ChatPopup chatPopup;
    protected PopUpManager manager;
    protected float timeFromStart;
    public bool isSingle;
    public virtual void Initialized(ChatDataDetail data, ChatPopup chatPopup, PopUpManager manager, bool muteSound = false)
    {
        timeFromStart = Time.time;
        this.manager = manager;
        this.chatPopup = chatPopup;
        this.data = data;
    }

    protected IEnumerator UpdateLayoutGroup()
    {
        yield return new WaitForEndOfFrame();
        /*Canvas.ForceUpdateCanvases();
        yield return new WaitForEndOfFrame();
        gameObject.GetComponent<VerticalLayoutGroup>().enabled = false; // **
        gameObject.GetComponent<VerticalLayoutGroup>().enabled = true;*/


    }
}
