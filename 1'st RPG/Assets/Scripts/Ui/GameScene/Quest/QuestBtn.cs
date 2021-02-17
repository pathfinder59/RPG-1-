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
            EventManager.Emit("CloseQuestPage");

            DialogManager.Instance.Interact(GameSceneManager.Instance.ActionObject);
        }
    }
    void ExitQuestPage()
    {
        QuestContent.ResetPointer();
        EventManager.Emit("CloseQuestPage");
        GameSceneManager.Instance.SetIsAct(false);
        GameSceneManager.Instance.SetInterctBtnAct(true);
    }

}
