using common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : Singleton<UiController>
{
    [SerializeField]
    GameObject _playUi;
    [SerializeField]
    GameObject _storePage;
    [SerializeField]
    GameObject _questPage;
    [SerializeField]
    GameObject _dialogPage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetActivePlayUi(bool value)
    {
        _playUi.SetActive(value);
    }

    public void SetActiveStorePage(bool value)
    {
        _storePage.SetActive(value);
    }
    public void SetActiveQuestPage(bool value)
    {
        _questPage.SetActive(value);
    }
    public void SetActiveDialogPage(bool value)
    {
        _dialogPage.SetActive(value);
    }
    void OffPlayUi(bool value)
    {
        GameSceneManager.Instance.SetIsAct(value);
        SetActivePlayUi(!value);
    }


    public void OpenStorePage(bool value)
    {
        OffPlayUi(value);
        SetActiveStorePage(value);
    }

    public void OpenQuestPage(bool value)
    {
        OffPlayUi(value);
        SetActiveQuestPage(value);
        if (!value)
        {
            EventManager.Emit("CloseQuestPage");
            GameSceneManager.Instance.SetInterctBtnAct(true);
        }
    }

    public void OpenDialogPage(bool value)
    {
        OffPlayUi(value);
        SetActiveDialogPage(value);
    }
}
