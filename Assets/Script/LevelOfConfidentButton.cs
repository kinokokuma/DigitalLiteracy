using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelOfConfidentButton : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private Image image;
    private int index;

    public int Index => index;
    public Image Image => image;
    public Button Button => button;

    public void Initialized(int index)
    {
        this.index = index;
        //button.onClick.AddListener(() => ConfidentButton());
    }
    /*
    public void ConfidentButton()
    {
        print(index);
        TimeRecord.Instance.SaveRecord($"level_of_confident_{index}");
        for (int i = 0; i < levelOfConfidentButton.Length; i++)
        {
            if (levelOfConfidentButton[i].Index <= index)
            {
                levelOfConfidentButton[i].Image.color = new Color32(41, 96, 184, 1);
            }
            else
            {
                levelOfConfidentButton[i].Image.color = Color.white;
            }

        }
    }*/
}
