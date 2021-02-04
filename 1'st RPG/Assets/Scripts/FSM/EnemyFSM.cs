using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

using common;
public abstract class EnemyFSM : FSM, IDamagable
{

    public enum FuncState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }
    protected FuncState _state;

    protected float currentTime = 0;
    Stat _stat;
    CharacterController cc;
    protected Animator _animator;
    NavMeshAgent _navMeshAgent;

    [SerializeField]
    float findDistance;   //탐색 범위

    [SerializeField]
    float moveDistance;  //추적 범위

    [SerializeField]
    protected float attackDistance; //공격 범위

    [SerializeField]
    protected float attackDelay;

    [SerializeField]
    float hitTime;

    [SerializeField]
    Slider _hpBar;

    Vector3 _originPos;
    Quaternion _originRot;

    public int Hp { get { return _stat.Hp; } set { _stat.Hp = value; } }
    public int MaxHp { get { return _stat.MaxHp; } }
    public int Atk => _stat.Atk;
    
    void Awake()
    {
        
        cc = GetComponent<CharacterController>();
        _animator = transform.GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _stat = GetComponent<Stat>();
        //string str = name.Substring(0, name.IndexOf('('));
        //str += "Status";
    }
    void Start()
    {
        EventManager.On("TrackEnemy",TrackEnemy);
    }

    void OnEnable()
    {
        _animator.SetTrigger("Reset");
        gameObject.layer = LayerMask.NameToLayer("Enemy");
        _state = FuncState.Idle;
        Hp = MaxHp;

        _target = null;  //변경할것임

        SetOriginTransform();
    }
    public void SetOriginTransform()
    {
        _originPos = transform.position;
        _originRot = transform.rotation;
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
            case FuncState.Attack:
                Attack();
                break;
            case FuncState.Return:
                Return();
                break;
            case FuncState.Damaged:
                break;
            case FuncState.Die:
                break;
        }
    }

    void Idle()
    {
        if (_target != null)
        {
            _state = FuncState.Move;
            print("상태 전환: Idle -> Move");

            _animator.SetTrigger("Move");
        }
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, _target.position) > moveDistance)
        {
            _state = FuncState.Return;
            print("상태 전환: Move -> Return");
            _target = null;
        }
        else if (Vector3.Distance(transform.position, _target.position) > attackDistance + enemyWidth)
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.ResetPath();
            _navMeshAgent.stoppingDistance = attackDistance + enemyWidth;

            _navMeshAgent.destination = _target.position;
        }
        else
        {

            _state = FuncState.Attack;
            print("상태 전환: Move -> Attack");

            _animator.SetTrigger("Attack");           
        }
    }

    public virtual void Attack()
    {
        if (Vector3.Distance(transform.position, _target.position) < attackDistance + enemyWidth)
        {           
            if (currentTime == 0)
            {
                transform.LookAt(_target);
                print("공격");
                currentTime = attackDelay;
                _animator.SetTrigger("StartAttack");
                //StartCoroutine("AttackEffect");
            }
        }
        else
        {
            _state = FuncState.Move;
            print("상태 전환: Attack -> Move");
            _animator.SetTrigger("Move");
        }
    }

    public abstract void AttackEvent();

    void Return()
    {
        if (Vector3.Distance(transform.position, _originPos) > 0.1f)
        {
            _navMeshAgent.destination = _originPos;
            _navMeshAgent.stoppingDistance = 0;
        }
        else
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.ResetPath();

            transform.position = _originPos;
            transform.rotation = _originRot;

            Hp = MaxHp;

            _state = FuncState.Idle;
            print("상태 전환: Return -> Idle");

            _animator.SetTrigger("Idle");
        }
    }

    public void Damaged(int hitPower,Transform enemy) // 이부분에 매개변수로 데미지를 주는 객체를 넣도록 해서 쫒아가도록 하자(_target에 대입)
    {
        EventManager.Emit("TrackEnemy",enemy.gameObject);
        if (_state == FuncState.Damaged || _state == FuncState.Die || _state == FuncState.Return)
        {
            return;
        }

        Hp -= hitPower;

        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();

        var go = ParticlePoolManager.Instance.Spawn("PopUpText", transform.position + new Vector3(0,3,0));
        go.GetComponentInChildren<TextMesh>().text = hitPower.ToString();

        if (Hp > 0)
        {
            _target = enemy;
            if(hitPower == 0)
            {
                if (_state != FuncState.Attack)
                {
                    _state = FuncState.Move;
                    _animator.SetTrigger("Move");
                }
            }
            else if (MaxHp/5 <= hitPower)
            {
                _state = FuncState.Damaged;
                print("상태 전환: Any State -> Damaged");
                _animator.SetTrigger("Damaged");
                StartCoroutine(DamageProcess());
            }
        }
        else
        {
            _state = FuncState.Die;
            print("상태 전환: Any State -> Die");
            _animator.SetTrigger("Die");
            Die(enemy);
        }
    }


    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(hitTime);

        _state = FuncState.Move;
        print("상태 전환: Damaged -> Move");
        //데미지 애니메이션이 실행될 경우 끝나면 바로 무브로 넘어가기때문에 트리거를 따로 설정하지 않아도 됨
        //이 부분을 바꿀 필요가 잇음
    }
    void DieProcess()
    {
        gameObject.SetActive(false);
    }

    
    void Die(Transform enemy)
    {
        StopAllCoroutines();
        enemy.GetComponent<FSM>().AddExp(_stat.MaxExp);
        gameObject.layer = LayerMask.NameToLayer("Die");
        var fsm = enemy.GetComponent<FSM>();
        if (fsm != null)
            fsm.FindTarget(5, 1 << LayerMask.NameToLayer("Enemy"));

        StartCoroutine(LateDie(enemy));
        //StartCoroutine(DieProcess());
    }

    abstract public IEnumerator LateDie(Transform enemy);

    void TrackEnemy(GameObject enemy)
    {
        if(Target != null)
            return;

        if(Vector3.Distance(enemy.transform.position, transform.position) < 5.0f)
            Target = enemy.transform;
        
    }
}
