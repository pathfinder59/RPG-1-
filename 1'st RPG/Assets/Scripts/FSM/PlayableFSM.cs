using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene.Skill;
using common;
public abstract class PlayableFSM : MonoBehaviour
{

    protected Animator _animator;

    PlayerMove _move;

    PlayerStatus _status;
    public PlayerStatus Status { get { return _status; } set { _status = value; } }

    GameObject _target;
    public GameObject Target { get { return _target; } set { _target = value; } }

    public enum FuncState
    {
        Idle, Move, Chase, Attack
    }
    FuncState _state;
    
    public bool isUsingSkill = false;

    private float currentTime;  //현재 공격 쿨타임을 나타냄
    public float attackDelay;  //공격 딜레이 길이

    public float attackDistance;
    public float movePower;


    private void Start()
    {
        _target = null;
        if (gameObject.name == "Player")
        {
            _status = PlayerManager.Instance._playerStatus;
            _move = gameObject.GetComponent<PlayerMove>();
        }
        else
            LoadData();
        _animator = GetComponent<Animator>();
        currentTime = 0.0f;

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

    private void Update()
    {
        currentTime = Mathf.Clamp(currentTime - Time.deltaTime, 0, attackDelay);

        switch (_state)
        {
            case FuncState.Idle:
                Idle();
                break;
            case FuncState.Move:
                Move();
                break;
            case FuncState.Chase:
                Chase();
                break;
            case FuncState.Attack:
                Attack();
                break;
        }
    }



    public void Idle()
    {
        if (isUsingSkill)
            return;
        if (name == "Player")
        {
            if (_move.Move(movePower))
            {
                _state = FuncState.Move;
                _animator.SetTrigger("Move");
                return;
            }
        }

        if (_target != null)
        {
            _state = FuncState.Chase;
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
                if (_target == null)
                {
                    _state = FuncState.Idle;
                    _animator.SetTrigger("Idle");

                }
                else
                    _state = FuncState.Chase;
                return;
            }
        }
    }

    void Chase()
    {
        if (isUsingSkill)
            return;
        if (_target == null || !Vector3.Equals(_move.ValueInputMove(), new Vector3(0, 0, 0)))  //플레이어인 경우에만
        {
            _state = FuncState.Move;
            //_animator.SetTrigger("Idle");
            _move.StopChase();
            return;

        }
        else
        {
            if (Vector3.Distance(gameObject.transform.position, _target.transform.position) <= attackDistance)
            {
                _state = FuncState.Attack;
                _animator.SetTrigger("Idle");
            }
            _move.Chase(_target.transform.position, attackDistance);
        }
    }

    void Attack()
    {
        if (isUsingSkill)
            return;
        if (_target == null || !Vector3.Equals(_move.ValueInputMove(), new Vector3(0, 0, 0)))
        {
            _state = FuncState.Move;
            _animator.SetTrigger("Move");
            _move.StopChase();
            return;
        }
        if (Vector3.Distance(gameObject.transform.position, _target.transform.position) <= attackDistance)
        {
            if (currentTime == 0.0f)
            {

                currentTime = attackDelay;
                gameObject.transform.LookAt(_target.transform);
                _animator.SetTrigger("Attack");
                StartCoroutine("AttackEffect");
            }
        }
        else
        {
            _state = FuncState.Chase;
            _animator.SetTrigger("Move");
        }
    }

    abstract public IEnumerator AttackEffect();
    abstract public IEnumerator SkillEffect();

    public IEnumerator UseSkill(SkillData data)
    {

        //이부분에서 스킬 동작 함수 변경하는게 맞는듯 startegy 패턴
        _state = FuncState.Idle;
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
}
