using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPopup : BasePopUp
{
    // Start is called before the first frame update
    [SerializeField]
    private string nextDataName;
    void Start()
    {
       StartCoroutine(ShowAllChatGuide());
    }
    private IEnumerator ShowAllChatGuide()
    {
        print(nextDataName);
        yield return new WaitForSeconds(3);

        manager.OpenChat(nextDataName, true);
        gameObject.SetActive(false);
    }
}
