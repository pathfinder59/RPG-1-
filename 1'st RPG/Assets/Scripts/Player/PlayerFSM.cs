using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFSM : MonoBehaviour
{

    PlayerStatus _status;
    public PlayerStatus Status => _status;
    
    enum PlayerState
    {
        Idle,Move,Attack,Passive,Active,Heal
    }

    PlayerState _state;
    void Start()
    {
        _state = PlayerState.Idle;
        _status = new PlayerStatus();
        LoadData();
    }
    void LoadData()
    {
        //로그파일 없으면 아래 기본값으로, 있으면 로그파일에 저장된 값으로 데이터 저장
        _status.Hp = 150;
        _status.MaxHp = 150;
        _status.Level = 1;
        _status.Name = "player";

        _status.SkillPoint = 0;
        _status.PassiveLevel = 0;
        _status.ActiveLevel = 0;
        _status.HealLevel = 0;

        _status.Exp = 0;
        _status.MaxExp = 100;
    }


    void Update()
    {
        
        switch(_state)
        {
            case PlayerState.Idle:
                break;
            case PlayerState.Move:
                break;
            case PlayerState.Attack:
                break;
            case PlayerState.Passive:
                break;
            case PlayerState.Active:
                break;
            case PlayerState.Heal:
                break;
                   
        }
    }

    void Idle()
    {

    }

    void Move()
    {

    }
    
    void Attack()
    {

    }
    void Passive()
    {

    }
    void Active()
    {

    }
    void Heal()
    {

    }
}
