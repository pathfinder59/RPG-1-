using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using common;
using GameScene.Skill;
public class PlayerFSM : MonoBehaviour
{

    Animator _animator;

    PlayerStatus _status;    public PlayerStatus Status => _status;
    PlayerMove _move;
    float movePower;


    public GameObject target;

    enum PlayerState
    {
        Idle,Move,Chase,Attack,UseSkill
    }

    PlayerState _state;
    public bool isUsingSkill = false;

    private float currentTime;  //현재 공격 쿨타임을 나타냄
    private float attackDelay;  //공격 딜레이 길이


    //move,idle함수를 담고있는 인터페이스를 만들어서 디벞 or 벞 상태에서의 동작들을 담는 인터페이스를 만들자.
    void Start()
    {
        movePower = 10;

        target = null;
        isUsingSkill = false;
        _state = PlayerState.Idle;

        if(gameObject.name == "Player")
        {
            _status = PlayerManager.Instance._playerStatus;
        }
        else
           LoadData();

        _animator = GetComponent<Animator>();
        _move = gameObject.GetComponent<PlayerMove>();

        currentTime = 0.0f;
        attackDelay = 2.0f;
    }
    void LoadData()
    {
        _status = new PlayerStatus();

        //로그파일 없으면 아래 기본값으로, 있으면 로그파일에 저장된 값으로 데이터 저장
        _status.Hp = 150;
        _status.MaxHp = 150;
        _status.Level = 1;
        _status.Name = "player";

        _status.ClassType = "Warrior"; // 임시로 클래스는 자동으로 워리어를 갖도록 함 이후
        // 캐릭터 추가까지 완성된다면 이부분은 바뀔것임

        _status.SkillPoint = 3;

        _status.Exp = 0;
        _status.MaxExp = 100;
    }


    void Update()
    {

        if (currentTime >= 0.0f)
            currentTime -= Time.deltaTime;

        switch(_state)
        {
            case PlayerState.Idle:
                Idle();
                break;
            case PlayerState.Move:
                Move();
                break;
            case PlayerState.Chase:
                Chase();
                break;
            case PlayerState.Attack:
                Attack();
                break;
            
        }
    }

    void Idle()
    {
        if (isUsingSkill)
            return;
        if (name == "Player")
        {
            if (_move.Move(movePower))
            {
                _state = PlayerState.Move;
                _animator.SetTrigger("Move");
                return;
            }
        }

        if(target != null)
        {
            _state = PlayerState.Chase;
            _animator.SetTrigger("Move");
        }
    }
    void Move()
    {
        if (isUsingSkill)
            return;
        if (name == "Player")  //플레이어인 경우에만
        {
            if (!_move.Move(movePower))
            {
                if (target == null)
                {
                    _state = PlayerState.Idle;
                    _animator.SetTrigger("Idle");

                }
                else
                    _state = PlayerState.Chase;
                return;
            }
        }
    }

    void Chase()
    {
        if (isUsingSkill)
            return;
        if (target == null || !Vector3.Equals(_move.ValueInputMove(), new Vector3(0,0,0) ))  //플레이어인 경우에만
        {
            _state = PlayerState.Move;
            //_animator.SetTrigger("Idle");
            _move.StopChase();
            return;

        }
        else
        {
            if(Vector3.Distance(gameObject.transform.position,target.transform.position) <= 3.0f)
            {
                _state = PlayerState.Attack;
                _animator.SetTrigger("Idle");
            }
            _move.Chase(target.transform.position, 3.0f);
        }
    }

    void Attack()
    {
        if (isUsingSkill)
            return;
        if(target == null || !Vector3.Equals(_move.ValueInputMove(), new Vector3(0, 0, 0)))
        {
            _state = PlayerState.Move;
            _animator.SetTrigger("Move");
            _move.StopChase();
            return;
        }
        if (Vector3.Distance(gameObject.transform.position, target.transform.position) <= 3.0f)
        {
            if (currentTime < 0.0f)
            {
                
                currentTime = attackDelay;
                gameObject.transform.LookAt(target.transform);
                _animator.SetTrigger("Attack");
            }
        }
        else
        {
            _state = PlayerState.Chase;
            _animator.SetTrigger("Move");
        }
    }


    public IEnumerator UseSkill(SkillData data)
    {

        //이부분에서 스킬 동작 함수 변경하는게 맞는듯 startegy 패턴
        _state = PlayerState.Idle;
        _animator.SetTrigger("Idle");
        _move.StopChase();

        
        

        isUsingSkill = true;

        if (data._trigger != null)
        {
            _animator.SetTrigger(data._trigger);
        }

        yield return new WaitForSeconds(data._time);
        isUsingSkill = false;
    }

    //스킬 시전 도중에 발생하는 이벤트함수는 코루틴으로 생성할까?
}
