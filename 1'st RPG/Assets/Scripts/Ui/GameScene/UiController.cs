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
        if(!value)
            EventManager.Emit("CloseQuestPage");
    }
    public void SetActiveDialogPage(bool value)
    {
        _dialogPage.SetActive(value);
    }

    public void OpenStorePage(bool value)
    {
        GameSceneManager.Instance.SetIsAct(value);
        SetActivePlayUi(!value);
        SetActiveStorePage(value);
    }

    public void OpenQuestPage(bool value)
    {
        GameSceneManager.Instance.SetIsAct(value);
        SetActivePlayUi(!value);
        SetActiveQuestPage(value);
    }

    public void OpenDialogPage(bool value)
    {
        GameSceneManager.Instance.SetIsAct(value);
        SetActivePlayUi(!value);
        SetActiveDialogPage(value);
    }
}
