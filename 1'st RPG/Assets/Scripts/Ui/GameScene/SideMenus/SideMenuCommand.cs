using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMenuCommand : MonoBehaviour,ICommand
{
    [SerializeField]
    GameObject _sideContent;

    static ICommand curCommand;
    public void Off()
    {
        _sideContent.gameObject.SetActive(false);
    }

    public void On()
    {
        _sideContent.gameObject.SetActive(true);
    }


    void Start()
    {
        if (curCommand != null)
            curCommand.Off();
        curCommand = this;
        curCommand.On();
    }

    void Update()
    {
        
    }

    public void OnClick()
    {
        if (curCommand == this as ICommand)
            return;
        curCommand.Off();
        curCommand = this;
        curCommand.On();
    }
}
