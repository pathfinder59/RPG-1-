using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene.Skill;
using common;
using UnityEngine.UI;

public abstract class PlayableFSM : FSM, IDamagable
{

    protected Animator _animator;

    PlayerMove _move;

    PlayerStatus _status;
    public PlayerStatus Status { get { return _status; } set { _status = value; } }

    public int Hp { get { return _status.Hp; } set { _status.Hp = value; } }
    public int MaxHp { get { return _status.MaxHp; }}

    public enum FuncState
    {
        Idle, Move, Chase, Attack, Damaged,Die
    }
    FuncState _state;
    
    public bool isUsingSkill = false;

    private float currentTime;  //현재 공격 쿨타임을 나타냄

    [SerializeField]
    float attackDelay;  //공격 딜레이 길이

    [SerializeField]
    float attackDistance;
    [SerializeField]
    float movePower;

    [SerializeField]
    float dieTime;

    [SerializeField]
    Slider _hpBar;
    void Awake()
    {      
        _animator = GetComponent<Animator>();
    }
    void Start()
    {
        if (gameObject.name == "Player")
        {
            _status = PlayerManager.Instance._playerStatus;
            _move = gameObject.GetComponent<PlayerMove>();
        }
        else
            LoadData();
    }

    void OnEnable()
    {
        //_animator.SetTrigger("Reset");
        gameObject.layer = LayerMask.NameToLayer("Player");
        _target = null;
        currentTime = 0.0f;
        if(_status != null)
            Hp = MaxHp;
    }

    void LoadData()
    {
        _status = new PlayerStatus();

        //로그파일 없으면 아래 기본값으로, 있으면 로그파일에 저장된 값으로 데이터 저장
        Hp = 150;
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
        _hpBar.value = (float)Hp / MaxHp;
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
        if (_target == null || !_move.ValueInputMove().Equals( new Vector3(0, 0, 0)))  //플레이어인 경우에만
        {
            _state = FuncState.Move;
            _move.StopChase();
            return;

        }
        else
        {
            if (Vector3.Distance(gameObject.transform.position, _target.transform.position) <= attackDistance)
            {
                _state = FuncState.Attack;
                _animator.SetTrigger("Attack");
            }
            else
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
                _animator.SetTrigger("AttackStart");
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
    

    public IEnumerator UseSkill(SkillData data)
    {

        //이부분에서 스킬 동작 함수 변경하는게 맞는듯 startegy 패턴
        _state = FuncState.Idle;
        _animator.SetTrigger("Idle");
        _move.StopChase();

        isUsingSkill = true;

        if (data.Trigger != null)
        {
            _animator.SetTrigger(data.Trigger);
        }
        StartCoroutine(data.Trigger); //각 자식 클래스에서 스킬이 있을경우 스킬 이름과 동일한 코루틴을 만들어 둘것! , 공통 스킬은 여기에 만들어 둔다

        yield return new WaitForSeconds(data.Time);
        isUsingSkill = false;
    }

    public void Damaged(int hitPower, Transform enemy)
    {
        if (_state == FuncState.Damaged || _state == FuncState.Die)
        {
            return;
        }

        Hp -= hitPower;

        _move.Agent.isStopped = true;
        _move.Agent.ResetPath();
        if (Hp <= 0)
        {
            _state = FuncState.Die;
            print("상태 전환: Any State -> Die");
            _animator.SetTrigger("Die");
            Die(enemy);
        }
    }

    public override void AddExp(float exp)
    {
        _status.Exp += exp;
    }

    IEnumerator DieProcess(Transform enemy)
    {
        yield return new WaitForSeconds(dieTime);
        print("소멸!");
        gameObject.SetActive(false);
    }
    void Die(Transform enemy)
    {
        StopAllCoroutines();
        StartCoroutine("LateDie");
        StartCoroutine(DieProcess(enemy));
    }

    abstract public IEnumerator LateDie(Transform enemy);

}
