using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene.Ui
{
    public class SettingPage : MonoBehaviour
    {
        GameObject _settingButton;
        GameObject _statusPage;
        void Start()
        {
            _settingButton = GameObject.Find("GameSceneCanvas").transform.Find("SettingButton").gameObject;
            _statusPage = GameObject.Find("GameSceneCanvas").transform.Find("StatusPage").gameObject;
        }

        void Update()
        {

        }


        public void OnClickBackButton()
        {
            _settingButton.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnClickStatusButton()
        {
            _statusPage.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnClickExitButton()
        {
            Application.Quit();
        }

    }
}