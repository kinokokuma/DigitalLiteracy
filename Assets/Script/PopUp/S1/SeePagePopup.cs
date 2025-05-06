using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeePagePopup : BasePopUp
{
    [SerializeField]
    private string nexStoryID;
    [SerializeField]
    private string nexStoryID2;
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Button backButton2;
    private float startTime;
    void Start()
    {
        StartCoroutine(showButton());
        backButton.onClick.AddListener(onClick);
        backButton2.onClick.AddListener(onClick2);
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

    private void onClick2()
    {
        manager.OpenChat(nexStoryID2, true);
        TimeRecord.Instance.SaveRecord("web1", "ออกจากเว็บ", startTime);
        gameObject.SetActive(false);

    }
}
