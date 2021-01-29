using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using common;

namespace GameScene.Ui
{
    public class StatusPage : MonoBehaviour
    {
        
        PlayerStat _playerStat;

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


            _playerStat = PlayerManager.Instance._playerStat;
            _nameText.text =  "Name: " + _playerStat.Name;
        }

        void Update()
        {
            _levelText.text = string.Format("Level: {0}", _playerStat.Level);
            _hpText.text = string.Format("Hp {0}/{1}", _playerStat.Hp, _playerStat.MaxHp);
            _expText.text = string.Format("Exp {0}/{1}", _playerStat.Exp, _playerStat.MaxExp);
        }

        public void OnClickBack()
        {
            _settingPage.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}