using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bank1Popup : BasePopUp
{
    public GameObject bank;
    public GameObject home;
    public GameObject p1;
    public GameObject p2;
    private float startTime;
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    public void OpenBank()
    {
        TimeRecord.Instance.SaveRecord(ID, "เปิดธนาคาร", startTime);
        StartCoroutine(open());
    }

    public void save(string comment)
    {
        if (comment == "ไม่โอนเงิน" || comment == "โอนเงิน")
        {

            TimeRecord.Instance.SaveRecord(ID, comment, startTime, true);
        }
        else
        {
            TimeRecord.Instance.SaveRecord(ID, comment, startTime, false);
        }
        
    }

    IEnumerator open()
    {
        bank.SetActive(true);
        yield return new WaitForSeconds(3);
        startTime = Time.time;
        home.SetActive(true);
    }

    public void Tran()
    {
        StartCoroutine(TranCount());
    }

    IEnumerator TranCount()
    {
        p1.SetActive(true);
        yield return new WaitForSeconds(3);
        startTime = Time.time;
        p2.SetActive(true);
    }

    public void back(string name)
    {
        manager.OpenChat(name);
        gameObject.SetActive(false);
    }
}
