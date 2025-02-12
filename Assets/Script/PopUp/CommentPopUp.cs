using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CommentPopUp : BasePopUp
{
    [SerializeField]
    private TMP_Text userName;
    [SerializeField]
    private TMP_Text userTime;
    [SerializeField]
    private TMP_Text commentText;
    [SerializeField]
    private TMP_Text shareText;
    [SerializeField]
    private Image postImage;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private RectTransform bg;
    [SerializeField]
    private GameObject reload;
    [SerializeField]
    private GameObject imageParent;
    private int refresh = 0;

    public void Initialized(PostData data, PopUpManager json, bool isHintDescription = true)
    {
        commentText.text = data.Description;
        userTime.text = data.Time;
        userName.text = data.Name;
        var texture = Resources.Load<Texture2D>($"Image/Icon/{data.Icon}");
        icon.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);

        if (data.PostImage != string.Empty)
        {
            var poseTexture = Resources.Load<Texture2D>($"Image/PostImage/{data.PostImage}");
            imageParent.gameObject.SetActive(true);
            postImage.sprite = Sprite.Create(poseTexture, new Rect(0.0f, 0.0f, poseTexture.width, poseTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
        else
        {
            postImage.gameObject.SetActive(false);
        }
    }

    public void LateUpdate()
    {

        if (refresh < 2)
        {
            bg.sizeDelta = new Vector2(bg.sizeDelta.x, commentText.rectTransform.sizeDelta.y + 45);
            StartCoroutine(UpdateLayoutGroup());
            refresh++;
            
        }
    }
}
