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
        if(QuestContent.clickedContent!= null)
        {
            EventManager.Emit("CloseQuestPage");

            DialogManager.Instance.Interact();
        }
    }
    public void ClickExitBtn()
    {
        QuestContent.clickedContent = null;
        EventManager.Emit("CloseQuestPage");
    }
}
