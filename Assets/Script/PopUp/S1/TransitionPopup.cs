using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PopupType
{
    chat,
    dialog,
    page
}

public class TransitionPopup : BasePopUp
{
    // Start is called before the first frame update
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
        StartCoroutine(ShowAllChatGuide());
    }
    private IEnumerator ShowAllChatGuide()
    {
        print(nextDataName);
        yield return new WaitForSeconds(3);

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
