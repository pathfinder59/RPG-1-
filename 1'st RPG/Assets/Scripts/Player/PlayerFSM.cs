using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using common;
using GameScene.Skill;
public class PlayerFSM : MonoBehaviour
{

    PlayerStatus _status;
    Animator _animator;
    PlayerMove _move;
    public PlayerStatus Status => _status;

    
    enum PlayerState
    {
        Idle,Move,Attack,UseSkill
    }

    PlayerState _state;
    public bool isUsingSkill = false;

    void Start()
    {
        isUsingSkill = false;
        _state = PlayerState.Idle;
        _status = new PlayerStatus();

        if(gameObject.name == "Player")
        {
            _status = PlayerManager.Instance._playerStatus;
        }
        else
           LoadData();

        _animator = GetComponent<Animator>();
        _move = gameObject.GetComponent<PlayerMove>();
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
                Idle();
                break;
            case PlayerState.Move:
                Move();
                break;
            case PlayerState.Attack:
                break;
            case PlayerState.UseSkill:
                break;
                   
        }
    }

    void Idle()
    {
        if (isUsingSkill)
            return;
        if (name == "Player")
        {
            if (_move.Move())
            {
                _state = PlayerState.Move;
                _animator.SetTrigger("Move");
            }
        }
    }
    void Move()
    {
        if (isUsingSkill)
            return;
        if (name == "Player")  //플레이어인 경우에만
        {
            if (!_move.Move())
            {
                _state = PlayerState.Idle;
                _animator.SetTrigger("Idle");
            }
        }
    }

    void Attack()
    {
        if (isUsingSkill)
            return;
    }


    public IEnumerator UseSkill(SkillData data)
    {
        isUsingSkill = true;
        _state = PlayerState.Idle;

        if(data._trigger != null)
        {
            _animator.SetTrigger(data._trigger);
        }

        yield return new WaitForSeconds(data._time);
        isUsingSkill = false;
    }
}
