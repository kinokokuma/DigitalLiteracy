using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OpenPage : MonoBehaviour
{
    
    public void OpenTutorial(GameObject obj)
    {
        obj.SetActive(true);
    }

    public void OpenScene(int index)
    {
        if(UserData.UserPass)
        SceneManager.LoadScene(index);
    }
}
