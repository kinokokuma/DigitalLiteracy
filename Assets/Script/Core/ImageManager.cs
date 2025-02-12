using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class ImageManager : MonoSingleton<ImageManager>
{
    private Dictionary<string, Sprite> imageDic = new Dictionary<string, Sprite>();
    private string jsonURL = "https://drive.google.com/uc?export=download&id=1B_YC58hLS9wa3WP8cBvWHlleAXfK01i6";

    public void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.LoadScene(1);
    }
    public Sprite LoadImage(string path)
    {

        if (!imageDic.ContainsKey(path))
        {
            // byte[] bytes = File.ReadAllBytes(path);
            // Texture2D poseTexture = new Texture2D(1, 1, TextureFormat.ARGB32, true);
            // poseTexture.LoadImage(bytes);
            Texture2D poseTexture = Resources.Load<Texture2D>(path);
            print(path);
            Sprite sprite = Sprite.Create(poseTexture, new Rect(0.0f, 0.0f, poseTexture.width, poseTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
            imageDic[path] = sprite;

            return sprite;
        }
        else
        {
            return imageDic[path];
        }
    }
    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/Test";
#elif UNITY_ANDROID
        return Application.persistentDataPath ;
#elif UNITY_IPHONE
        return Application.persistentDataPath;
#else
        return  "/CSV/" + UserData.Story + "_" + UserData.Solution+ "_" + UserData.UserID + ".csv";
#endif
        Debug.Log("get path leido");
    }
}

