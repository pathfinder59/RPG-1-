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

        void Start()
        {
            _playerStatus = _player.GetComponent<PlayerFSM>().Status;
            _nameText.text =  "Name: "+ _playerStatus.Name;
        }

        void Update()
        {
            _levelText.text = string.Format("Level {0}", _playerStatus.Level);
            _activeCount.text = _playerStatus.ActiveLevel.ToString();
            _passiveCount.text = _playerStatus.PassiveLevel.ToString();
            _healCount.text = _playerStatus.HealLevel.ToString();
            _hpText.text = string.Format("Hp {0}/{1}",_playerStatus.Hp,_playerStatus.MaxHp);
            _expText.text = string.Format("Exp {0}/{1}", _playerStatus.Exp, _playerStatus.MaxExp);
        }
    }
}