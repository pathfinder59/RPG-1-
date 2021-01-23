using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingButton : MonoBehaviour
{

    GameObject SettingPage;
    void Start()
    {
        SettingPage = GameObject.Find("GameSceneCanvas").transform.Find("SettingPage").gameObject;    
    }

    void Update()
    {
        
    }

    public void OnClickSettingButton()
    {
        if (SettingPage)
        {
            SettingPage.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
