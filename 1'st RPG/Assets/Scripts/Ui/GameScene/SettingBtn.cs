using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingBtn : MonoBehaviour
{
    [SerializeField]
    GameObject SettingPage;

    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        SettingPage.SetActive(true);
        GameSceneManager.Instance.SetIsAct(true);
        gameObject.SetActive(false);
    }

}
