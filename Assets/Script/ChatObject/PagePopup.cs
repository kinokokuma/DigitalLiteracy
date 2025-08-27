using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PagePopup : BasePopUp
{
    [SerializeField]
    private string nextDataName;
    [SerializeField]
    private PopupType popupType;
    [SerializeField]
    private SoundID soundID;
    void Start()
    {
        if (soundID != SoundID.None)
        {
            SoundManager.Instance.PlaySound(soundID);
        }
    }
    public void EndPage()
    {

        if (popupType == PopupType.chat)
        {
            manager.OpenChat(nextDataName, true);
        }
        else if (popupType == PopupType.dialog)
        {
            manager.OpenDialog(nextDataName);
        }
        gameObject.SetActive(false);
    }
}