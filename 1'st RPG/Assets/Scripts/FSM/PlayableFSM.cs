using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene.Skill;
using common;
using UnityEngine.UI;
using UnityEngine.AI;

public abstract class PlayableFSM : FSM, IDamagable
{
    PlayerStat _stat;
  

    public PlayerStat Status { get => _stat;}

    public int AddAtk { get; set; }
    public int AddDef { get; set; }
    public int Hp { get { return _stat.Hp; } set { _stat.Hp = value; } }
    public int MaxHp { get { return _stat.MaxHp; }}

    public bool IsMoving { get; set; }
    
    public bool isUsingSkill = false;


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
        UpdataRoutine();
    }



    public override void Idle()
    {
        if (isUsingSkill)
            return;

        if (IsMoving)
        {
            _state = FuncState.Move;
            _animator.SetTrigger("Move");
            return;
        }

        base.Idle();
    }
    public override void Move()
    {
        if (isUsingSkill)
            return;

        if (!IsMoving)
        {
            if (_target == null)
            {
                _state = FuncState.Idle;
                _animator.SetTrigger("Idle");
                StopChase();
            }
            else
            {
                if (Vector3.Distance(transform.position, _target.position) > attackDistance + enemyWidth)
                {
                    Chase(_target.position, attackDistance + enemyWidth);
                }
                else
                {
                    _state = FuncState.Attack;
                    _animator.SetTrigger("Attack");
                }
            }
        }
        else
            StopChase();

    }

    public override void Attack()
    {
        if (isUsingSkill)
            return;
        if (_target == null)
        {
            _state = FuncState.Idle;
            _animator.ResetTrigger("StartAttack");
            _animator.SetTrigger("Idle");
            StopChase();
            return;
        }
        else if(IsMoving)
        {
            _state = FuncState.Move;
            _animator.ResetTrigger("StartAttack");
            _animator.SetTrigger("Move");
            StopChase();
            return;
        }
        base.Attack();
    }
    public abstract void AttackEvent();
    public abstract IEnumerator AttackEffect();
    

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

        _animator.SetTrigger(data.Name);

        yield return new WaitForSeconds(data.Time);  //스킬 애니메이션 길이
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
        _stat.Heal(-hitPower);

        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();

        if (_target == null)
        {
            _target = enemy;
            enemyWidth = _target.GetComponent<CharacterController>().radius;
        }
        SpawnDamagedText(hitPower);
        if (Hp <= 0)
        {
            _state = FuncState.Die;
            _animator.SetTrigger("Die");
            Die(enemy);
        }
        EventManager.Emit("UpdateStatus");
    }

    public override void AddExp(float exp, GameObject obj = null)
    {
        _stat.AddExp(exp);      
       
        if (obj != null)
        {
            if (obj.layer != LayerMask.NameToLayer("Enemy"))
                return;
            QuestManager mgr = QuestManager.Instance;


            foreach (KeyValuePair<int,Quest> valuePair in mgr.currentQuests[0])
            {
                if (valuePair.Value.targetId == obj.GetComponent<ObjData>().id)
                    valuePair.Value.DecreaseNum(1);
            }
            EventManager.Emit("UpdateDescriptor");
        }
        
    }

    void DieProcess(Transform enemy)
    {
        gameObject.SetActive(false);
    }
    void Die(Transform enemy)
    {
        StopAllCoroutines();
        StartCoroutine(LateDie(enemy));
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
}
