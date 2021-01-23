using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameScene.Ui
{
    public class StatusPage : MonoBehaviour
    {
        [SerializeField]
        GameObject _player;

        PlayerStatus _playerStatus;

        [SerializeField]
        Text _nameText;
        [SerializeField]
        Text _levelText;
        [SerializeField]
        Text _expText;
        [SerializeField]
        Text _hpText;

        [SerializeField]
        Text _activeCount;
        [SerializeField]
        Text _passiveCount;
        [SerializeField]
        Text _healCount;

        [SerializeField]
        Text _skillPoint;

        GameObject _settingPage;

        [SerializeField]
        GameObject _activeSkillUpBtn;
        [SerializeField]
        GameObject _passiveSkillUpBtn;
        [SerializeField]
        GameObject _healSkillUpBtn;
        void Start()
        {
            _settingPage = GameObject.Find("GameSceneCanvas").transform.Find("SettingPage").gameObject;


            _playerStatus = _player.GetComponent<PlayerFSM>().Status;
            _nameText.text =  "Name: " + _playerStatus.Name;
        }

        void Update()
        {
            _skillPoint.text = string.Format("SkillPoint: {0}", _playerStatus.SkillPoint);
            _levelText.text = string.Format("Level {0}", _playerStatus.Level);
            _activeCount.text = _playerStatus.ActiveLevel.ToString();
            _passiveCount.text = _playerStatus.PassiveLevel.ToString();
            _healCount.text = _playerStatus.HealLevel.ToString();
            _hpText.text = string.Format("Hp {0}/{1}",_playerStatus.Hp,_playerStatus.MaxHp);
            _expText.text = string.Format("Exp {0}/{1}", _playerStatus.Exp, _playerStatus.MaxExp);
        }

        public void OnClickBack()
        {
            _settingPage.SetActive(true);
            gameObject.SetActive(false);
        }

        public void OnClickActiveUp()
        {
            if(_playerStatus.SkillPoint != 0)
            {
                _playerStatus.ActiveLevel++;
                _playerStatus.SkillPoint--;

                if (_playerStatus.ActiveLevel >= 3)
                    _activeSkillUpBtn.SetActive(false);
            }
        }

        public void OnClickPassiveUp()
        {
            if (_playerStatus.SkillPoint != 0)
            {
                _playerStatus.PassiveLevel++;
                _playerStatus.SkillPoint--;

                if (_playerStatus.PassiveLevel >= 3)
                    _passiveSkillUpBtn.SetActive(false);
            }
        }

        public void OnClickHealUp()
        {
            if (_playerStatus.SkillPoint != 0)
            {
                _playerStatus.HealLevel++;
                _playerStatus.SkillPoint--;

                if (_playerStatus.HealLevel >= 3)
                    _healSkillUpBtn.SetActive(false);
            }
        }
    }
}