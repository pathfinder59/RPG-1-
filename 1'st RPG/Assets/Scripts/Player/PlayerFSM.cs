using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using common;
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
        if(gameObject.name == "Player")
        {
            _status = PlayerManager.Instance._playerStatus;
        }
        else
           LoadData();
    }
    void LoadData()
    {
        //로그파일 없으면 아래 기본값으로, 있으면 로그파일에 저장된 값으로 데이터 저장
        _status.Hp = 150;
        _status.MaxHp = 150;
        _status.Level = 1;
        _status.Name = "player";

        _status.ClassType = "Warrior"; // 임시로 클래스는 자동으로 워리어를 갖도록 함 이후
        // 캐릭터 추가까지 완성된다면 이부분은 바뀔것임

        _status.SkillPoint = 3;
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
