using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPage : MonoBehaviour
{

    GameObject _settingPage;
    void Start()
    {
        _settingPage = GameObject.Find("GameSceneCanvas").transform.Find("SettingPage").gameObject;
    }

    void Update()
    {
        
    }

    public void OnClickBack()
    {
        _settingPage.SetActive(true);
        gameObject.SetActive(false);
    }
}
