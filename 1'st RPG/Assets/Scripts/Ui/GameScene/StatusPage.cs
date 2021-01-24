using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using common;

namespace GameScene.Ui
{
    public class StatusPage : MonoBehaviour
    {
        
        PlayerStatus _playerStatus;

        [SerializeField]
        Text _nameText;
        [SerializeField]
        Text _levelText;
        [SerializeField]
        Text _expText;
        [SerializeField]
        Text _hpText;


        GameObject _settingPage;

        void Start()
        {
            _settingPage = GameObject.Find("GameSceneCanvas").transform.Find("SettingPage").gameObject;


            _playerStatus = PlayerManager.Instance._playerStatus;
            _nameText.text =  "Name: " + _playerStatus.Name;
        }

        void Update()
        {
            _levelText.text = string.Format("Level: {0}", _playerStatus.Level);
            _hpText.text = string.Format("Hp {0}/{1}",_playerStatus.Hp,_playerStatus.MaxHp);
            _expText.text = string.Format("Exp {0}/{1}", _playerStatus.Exp, _playerStatus.MaxExp);
        }

        public void OnClickBack()
        {
            _settingPage.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}