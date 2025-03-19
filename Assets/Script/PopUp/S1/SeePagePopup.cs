using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeePagePopup : BasePopUp
{
    [SerializeField]
    private string nexStoryID;
    [SerializeField]
    private Button backButton;
    private float startTime;
    void Start()
    {
        StartCoroutine(showButton());
        backButton.onClick.AddListener(onClick);
    }
    
    private IEnumerator showButton()
    {
        yield return new WaitForSeconds(3);
        backButton.gameObject.SetActive(true);
        startTime = Time.time;

    }
    private void onClick()
    {
        manager.OpenChat(nexStoryID,true);
        TimeRecord.Instance.SaveRecord("web1", "ออกจากเว็บ", startTime);
        gameObject.SetActive(false);
            
    }
}
