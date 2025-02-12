using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePopUp : MonoBehaviour
{
    public string ID;
    public float startTime;
    protected PopUpManager manager;
    public void SetManager(PopUpManager manager)
    {
        this.manager = manager;
    }

    public void save(string comment, bool isSignificant =false)
    {
        TimeRecord.Instance.SaveRecord(ID, comment, startTime,isSignificant);
    }

    protected IEnumerator UpdateLayoutGroup(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
         //   reloadObject.SetActive(false);
            yield return new WaitForEndOfFrame();

           // reloadObject.SetActive(true);
        }
        yield return new WaitForEndOfFrame();
        Canvas.ForceUpdateCanvases();
    }
}
