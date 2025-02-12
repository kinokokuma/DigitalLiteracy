using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadPopup : BasePopUp
{
    [SerializeField]
    private TMP_Text userName;
    [SerializeField]
    private TMP_Text userTime;
    [SerializeField]
    private TMP_Text commentText;
    [SerializeField]
    private TMP_Text likeText;
    [SerializeField]
    private Image icon;
    [SerializeField]
    private TMP_Text description;
    [SerializeField]
    private Image postImage;
    private PostData postData;
    [SerializeField]
    private CommentPopUp commentPrefab;
    [SerializeField]
    private Transform commentParent;
    [SerializeField]
    private RectTransform rectTransform;
    [SerializeField]
    private RectTransform scrollRectTransform;
    [SerializeField]
    private PostPopup pop;
    [SerializeField]
    private GameObject reloadObject;

    public void Initialized(PostData data, PopUpManager json)
    {
        postImage.gameObject.SetActive(false);
        description.text = data.Description;
        var texture = Resources.Load<Texture2D>($"Image/Icon/{data.Icon}");
        icon.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);
        userName.text = data.Name;
        postData = data;
        userTime.text = data.Time;
        likeText.text = data.LikeCount.ToString();
        commentText.text = $" {data.ShereCount} แชร์";

        if (data.PostImage != string.Empty)
        {
            print(data.PostImage);
            postImage.gameObject.SetActive(true);
            var poseTexture = Resources.Load<Texture2D>($"Image/PostImage/{data.PostImage}");
            postImage.sprite = Sprite.Create(poseTexture, new Rect(0.0f, 0.0f, poseTexture.width, poseTexture.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
        
        if (postData.CommentData != null)
        {
            foreach (var commentData in postData.CommentData)
            {
                print(commentData);
                var popup = Instantiate(commentPrefab, commentParent);
                popup.gameObject.SetActive(true);
                popup.Initialized(commentData, json);
                StartCoroutine(Set(popup.gameObject));
            }
        }
        StartCoroutine(UpdateLayoutGroup( 3));

    }

    public void Confirm()
    {
       
    }

    private IEnumerator Set(GameObject popup)
    {
        yield return new WaitForSeconds(0.1f);
        popup.SetActive(false);
        popup.SetActive(true);
    }
}
