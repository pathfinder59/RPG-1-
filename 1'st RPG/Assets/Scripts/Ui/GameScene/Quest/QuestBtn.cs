using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using common;
public class QuestBtn : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ClickSelectBtn()
    {
        ActQuest();
    }
    public void ClickExitBtn()
    {
        ExitQuestPage();
    }


    void ActQuest()
    {
        if (QuestContent.clickedContent != null)
        {
            UiController.Instance.OpenQuestPage(false);
            DialogManager.Instance.Interact(GameSceneManager.Instance.ActionObject);
        }
    }
    void ExitQuestPage()
    {
        QuestContent.ResetPointer();
        UiController.Instance.OpenQuestPage(false);
    }

}
