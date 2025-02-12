using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : BasePopUp
{
    public GameObject[] Image;

    void Start()
    {
        StartCoroutine(Count());
    }

    IEnumerator Count()
    {
        for (int i = 0; i < Image.Length; i++)
        {
            yield return new WaitForSeconds(3);
            Image[i].SetActive(false);
        }

    }
    
    public void BackHome()
    {
        SceneManager.LoadScene(1);
    }
}
