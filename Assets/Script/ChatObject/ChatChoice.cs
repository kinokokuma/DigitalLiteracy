using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum ChoiceType
{
    String,
    Image
}

[RequireComponent(typeof(Button))]
public class ChatChoice : MonoBehaviour
{
    [SerializeField]
    private Button button;
    [SerializeField]
    private Image image;
    [SerializeField]
    private TMP_Text text;
    private int index;
    private ChoiceImage dataImage;
    private ChoiceText dataText;

    public int Index => index;
    public Button Button => button;
    public ChoiceImage DataImage => dataImage;
    public ChoiceText DataText => dataText;

    public void InitializedImage(int index, ChoiceType type, ChoiceImage data)
    {
        this.dataImage = data;
        this.index = index;

        if(type == ChoiceType.Image)
        {
            image.sprite = ImageManager.Instance.LoadImage(data.Path);
        }

        if(data.CanClick != null)
        {
            if (!data.CanClick)
            {
                button.interactable = false;
            }
        }
        else
        {
            button.interactable = true;
        }
    }

    public void InitializedText(int index, ChoiceType type, ChoiceText data)
    {
        this.dataText = data;
        this.index = index;
        text.text = data.Path;
    }
}
