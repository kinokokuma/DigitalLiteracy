using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestPopUp : BasePopUp
{
    public Button back;
    public Button[] go;
    public LevelOfConfidentButton[] levelOfConfidentButton;
    public Button[] seenButton;
    public Button confirm;
    public RectTransform score;
    public RectTransform select;
    public RectTransform seen;
    public PopUpManager manager;
    public RectTransform viewPoint;

    public void Start()
    {
        confirm.interactable = false;
        back.onClick.AddListener(() => 
        {
            //TimeRecord.Instance.SaveRecord("back_to_is_fake");
            Back(true);
            manager.SetPhase(QuestionPhase.Is_Fake);
        });

        go[0].onClick.AddListener(() => 
        {
          //  TimeRecord.Instance.SaveRecord("not_fake");
            Back(false);
            manager.SetPhase(QuestionPhase.Level_Of_Confident);

        });

        go[1].onClick.AddListener(() => 
        {
            //TimeRecord.Instance.SaveRecord("fake");
            Back(false);
            manager.SetPhase(QuestionPhase.Level_Of_Confident);
        });

        seenButton[0].onClick.AddListener(() =>
        {
            ResetConfidentButton();
          //  TimeRecord.Instance.SaveRecord("yes");
            Confirm();
        });

        seenButton[1].onClick.AddListener(() =>
        {
            ResetConfidentButton();
           // TimeRecord.Instance.SaveRecord("no");
            Confirm();
        });

        confirm.onClick.AddListener(() => {
          //  TimeRecord.Instance.SaveRecord("go_to_have_seen");
            seen.gameObject.SetActive(true);
            score.gameObject.SetActive(false);
            viewPoint.sizeDelta = new Vector2(1024, 1366) - new Vector2(0, seen.sizeDelta.y + 20);
            manager.SetPhase(QuestionPhase.Have_seen);
        });

        for(int i = 0; i < levelOfConfidentButton.Length; i++)
        {
            int x = i+1;
            LevelOfConfidentButton button = levelOfConfidentButton[i];
            button.Initialized(i);
            button.Button.onClick.AddListener(() => ConfidentButton(button));
        }
    }

    private void ConfidentButton(LevelOfConfidentButton button)
    {
        confirm.interactable = true;
        print(button.Index);
       // TimeRecord.Instance.SaveRecord($"level_of_confident_{button.Index + 1}");
        for(int i=0; i < levelOfConfidentButton.Length; i++)
        {
            if (levelOfConfidentButton[i].Index <= button.Index)
            {
                levelOfConfidentButton[i].Image.color = new Color32(41,96,184,255);
            }
            else
            {
                levelOfConfidentButton[i].Image.color = Color.white;
            }

        }
    }

    private void ResetConfidentButton()
    {
        confirm.interactable = false;
        for (int i = 0; i < levelOfConfidentButton.Length; i++)
        {
                levelOfConfidentButton[i].Image.color = Color.white;
        }
    }

    private void Confirm()
    {
        Back(true);
        viewPoint.sizeDelta = new Vector2(1024, 1227);
        seen.gameObject.SetActive(false);
        TimeRecord.Instance.AddTaskNumber();
        manager.Confirm();
        manager.SetPhase(QuestionPhase.Is_Fake);
    }

    private void Back(bool ok)
    {
        select.gameObject.SetActive(ok);
        score.gameObject.SetActive(!ok);

        if (ok)
        {
            viewPoint.sizeDelta = new Vector2(1024,1366) - new Vector2(0, select.sizeDelta.y + 20);
        }
        else
        {
            viewPoint.sizeDelta = new Vector2(1024, 1366) - new Vector2(0, score.sizeDelta.y+20);
        }
    }

}
