using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChatData
{
    public string ID;
    public string ChatName;
    public string[] Icon;
    public string Header;
    public ChatDataDetail[] DataDetail;

}

[System.Serializable]
public class ChatDataDetail
{
    public string ID = string.Empty;
    public string OnwerName = string.Empty;
    public string Icon = string.Empty;
    public string ChatType = string.Empty;
    public float DelayTime;
    public string Content = string.Empty;
    public string PostImage = string.Empty;
    public ChoiceImage[] ChoiceImage;
    public ChoiceText[] Choice;
    public string LinkToPageName = string.Empty;
    public string LinkType = string.Empty;
    public string FileName = string.Empty;
}

[System.Serializable]
public class ChoiceImage
{
    public string ID = string.Empty;
    public bool CanClick = true;
    public string Path = string.Empty;
    public string LinkType = string.Empty;
    public string FileName = string.Empty;
    public bool IsSignificant;
}

[System.Serializable]
public class ChoiceText
{
    public string ID = string.Empty;
    public string Path = string.Empty;
    public string LinkType = string.Empty; 
    public string FileName = string.Empty;
    public bool IsSignificant;
}

[System.Serializable]
public class DialogData
{
    public string ID = string.Empty;
    public string ChatName = string.Empty;
    public string BG = string.Empty;
    public DialogDataDetail[] DataDetail;

}

[System.Serializable]
public class DialogDataDetail
{
    public string ID = string.Empty;
    public string OnwerName = string.Empty;
    public string ChatSide = string.Empty;
    public DialogImage[] DialogImage;
    public string ChatType = string.Empty;
    public float DelayTime;
    public string Content = string.Empty;
    public string PostImage;
    public ChoiceImage[] ChoiceImage;
    public ChoiceText[] Choice;
    public string LinkToPageName = string.Empty;
    public string LinkType = string.Empty;
    public string FileName = string.Empty;
    public string Sound = string.Empty;
}

[System.Serializable]
public class DialogImage
{
    public string Image = string.Empty;
    public string ImageSide = string.Empty;
    public bool Active;
}
