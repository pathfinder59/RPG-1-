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
        [SerializeField]
        Text _atkText;
        [SerializeField]
        Text _defText;

        void Start()
        {
            _playerStat = PlayerManager.Instance._playerStat;
            _nameText.text =  "Name: " + _playerStat.Name;
            EventManager.On("UpdateStatus", UpdateStatusPage);
        }
        
       
        void Update()
        {
            _levelText.text = string.Format("Level: {0}", _playerStat.Level);
            _hpText.text = string.Format("Hp {0}/{1}", _playerStat.Hp, _playerStat.MaxHp);
            _expText.text = string.Format("Exp {0}/{1}", _playerStat.Exp, _playerStat.MaxExp);
            _atkText.text = string.Format("ATK {0}+({1})", _playerStat.Atk,PlayerManager.Instance.AddtiveAtk);
            _defText.text = string.Format("DEF {0}+({1})", _playerStat.Def, PlayerManager.Instance.AddtiveDef);
        }

        public void UpdateStatusPage(GameObject obj = null)
        {
            _levelText.text = string.Format("Level: {0}", _playerStat.Level);
            _hpText.text = string.Format("Hp {0}/{1}", _playerStat.Hp, _playerStat.MaxHp);
            _expText.text = string.Format("Exp {0}/{1}", _playerStat.Exp, _playerStat.MaxExp);
            _atkText.text = string.Format("ATK {0} + {1}", _playerStat.Atk, PlayerManager.Instance.AddtiveAtk);
            _defText.text = string.Format("DEF {0} + {1}", _playerStat.Def, PlayerManager.Instance.AddtiveDef);
        }
    }
}