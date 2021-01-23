using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene.Ui
{
    public class SettingPage : MonoBehaviour
    {
        GameObject _settingButton;
        void Start()
        {
            _settingButton = GameObject.Find("GameSceneCanvas").transform.Find("SettingButton").gameObject;
        }

        void Update()
        {

        }


        public void OnClickBackButton()
        {
            _settingButton.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}