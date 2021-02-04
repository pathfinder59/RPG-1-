using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene.Skill;
using common;
using UnityEngine.UI;
using UnityEngine.AI;

public abstract class PlayableFSM : FSM, IDamagable
{

    protected Animator _animator;

    PlayerStat _stat;
    NavMeshAgent _navMeshAgent;

    public PlayerStat Status { get => _stat;}

    public int AddAtk { get; set; }
    public int AddDef { get; set; }
    public int Hp { get { return _stat.Hp; } set { _stat.Hp = value; } }
    public int MaxHp { get { return _stat.MaxHp; }}

    public bool IsMoving { get; set; }
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
    float dieTime;

    [SerializeField]
    Slider _hpBar;

    void Awake()
    {
        AddAtk = 0;
        AddDef = 0;
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }
    void Start()
    {
        if (gameObject.name == PlayerManager.Instance._playerStat.Name)
        {
            _stat = PlayerManager.Instance._playerStat;
        }
        else
            LoadData();
    }

    void OnEnable()
    {
        IsMoving = false;
        _animator.SetTrigger("Reset");
    
        _target = null;
        currentTime = 0.0f;
        if(_stat != null)
            Hp = MaxHp;
        StartCoroutine("HealPerSecond");
    }

    void LoadData()
    {
        //_stat = new PlayerStatus();

        //로그파일 없으면 아래 기본값으로, 있으면 로그파일에 저장된 값으로 데이터 저장
        Hp = 150;
        _stat.MaxHp = 150;
        _stat.Level = 1;
        _stat.Name = "player";

        _stat.ClassType = "Warrior"; // 임시로 클래스는 자동으로 워리어를 갖도록 함 이후
        // 캐릭터 추가까지 완성된다면 이부분은 바뀔것임

        _stat.SkillPoint = 3;

        _stat.Exp = 0;
        _stat.MaxExp = 100;
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

        if (IsMoving)
        {
            _state = FuncState.Move;
            _animator.SetTrigger("Move");
            return;
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

        if (!IsMoving)
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

    void Chase()
    {
        if (isUsingSkill)
            return;
        if (_target == null || IsMoving)  //플레이어인 경우에만
        {
            _state = FuncState.Move;
            StopChase();
            return;

        }
        else
        {
            if (Vector3.Distance(gameObject.transform.position, _target.transform.position) <= attackDistance + enemyWidth)
            {
                _state = FuncState.Attack;
                _animator.SetTrigger("Attack");
            }
            else
                Chase(_target.transform.position, attackDistance + enemyWidth);
        }
    }

    void Attack()
    {
        if (isUsingSkill)
            return;
        if (_target == null)
        {
            _state = FuncState.Idle;
            _animator.ResetTrigger("AttackStart");
            _animator.SetTrigger("Idle");
            StopChase();
            return;
        }
        else if(IsMoving)
        {
            _state = FuncState.Move;
            _animator.ResetTrigger("AttackStart");
            _animator.SetTrigger("Move");
            StopChase();
            return;
        }

        if (Vector3.Distance(gameObject.transform.position, _target.transform.position) <= attackDistance + enemyWidth)
        {
            if (currentTime == 0.0f)
            {

                currentTime = attackDelay;
                gameObject.transform.LookAt(_target.transform);
                _animator.SetTrigger("AttackStart");
            }
        }
        else
        {
            _state = FuncState.Chase;
            _animator.SetTrigger("Move");
        }
    }
    public abstract void AttackEvent();
    abstract public IEnumerator AttackEffect();
    

    public IEnumerator UseSkill(SkillData data)
    {

        //이부분에서 스킬 동작 함수 변경하는게 맞는듯 startegy 패턴
        if (_state != FuncState.Idle)
        {
            _state = FuncState.Idle;
            _animator.SetTrigger("Idle");
        }
        StopChase();

        isUsingSkill = true;

        if (data.Name != null)
        {
            _animator.SetTrigger(data.Name);
        }
        //StartCoroutine(data.Trigger); //각 자식 클래스에서 스킬이 있을경우 스킬 이름과 동일한 코루틴을 만들어 둘것! , 공통 스킬은 여기에 만들어 둔다

        yield return new WaitForSeconds(data.Time);
        TurnOffSkill();
    }

    public void TurnOffSkill()
    {
        isUsingSkill = false;
    }

    public void Damaged(int hitPower, Transform enemy)
    {
        if ( _state == FuncState.Die)
        {
            return;
        }
        hitPower -= (AddDef + _stat.Def);
        Hp -= hitPower;

        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();

        if (_target == null)
        {
            _target = enemy;
            enemyWidth = _target.GetComponent<CharacterController>().radius;
        }
        var go = ParticlePoolManager.Instance.Spawn("PopUpText", transform.position + new Vector3(0, 3, 0));
        go.GetComponentInChildren<TextMesh>().text = hitPower.ToString();
        
        if (Hp <= 0)
        {
            _state = FuncState.Die;
            print("상태 전환: Any State -> Die");
            _animator.SetTrigger("Die");
            Die(enemy);
        }
        EventManager.Emit("UpdateStatus");
    }

    public override void AddExp(float exp)
    {
        if(_stat.AddExp(exp))
        {
            var go = ParticlePoolManager.Instance.Spawn("LevelUp");
            go.transform.position = transform.position;
            go.GetComponent<ParticleTime>().SetTarget(transform);
        }
        EventManager.Emit("UpdateStatus");
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

    IEnumerator HealPerSecond()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            Hp = Mathf.Clamp(Hp + MaxHp/100, 0, MaxHp);
        }
    }

    public void Chase(Vector3 target, float distance)
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();
        _navMeshAgent.stoppingDistance = distance;
        _navMeshAgent.destination = target;
    }

    public void StopChase()
    {
        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();
    }

}
