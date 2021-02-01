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
    FuncState _state;

    [SerializeField]
    float findDistance;   //탐색 범위
    [SerializeField]
    float moveDistance;  //추적 범위
    [SerializeField]
    float attackDistance; //공격 범위

    CharacterController cc;

    float currentTime = 0;
    [SerializeField]
    float attackDelay;

    Vector3 _originPos;
    Quaternion _originRot;

    int hp;
    public int Hp { get { return hp; } set { hp = value; } }
    [SerializeField]
    int maxHp;
    public int MaxHp { get { return maxHp; } }
    [SerializeField]
    float exp;
    


    [SerializeField]
    int atk; // 데미지
    public int Atk => atk;
    [SerializeField]
    float hitTime;
    [SerializeField]
    float dieTime;

    Animator _animator;
    NavMeshAgent _navMeshAgent;

    [SerializeField]
    Slider _hpBar;

    void Awake()
    {
        
        cc = GetComponent<CharacterController>();
        _animator = transform.GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _originPos = transform.position;
        _originRot = transform.rotation;

        string str = name.Substring(0, name.IndexOf('('));
        str += "Status";
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
        hp = maxHp;

        _target = null;  //변경할것임
    }

    void Update()
    {
        _hpBar.value = (float)hp / maxHp;
        

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

        //hpSlider.value = (float)hp / (float)maxHp;
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
        else if (Vector3.Distance(transform.position, _target.position) > attackDistance)
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.ResetPath();
            _navMeshAgent.stoppingDistance = attackDistance;

            _navMeshAgent.destination = _target.position;
        }
        else
        {

            _state = FuncState.Attack;
            print("상태 전환: Move -> Attack");

            _animator.SetTrigger("Attack");           
        }
    }

    void Attack()
    {
        if (Vector3.Distance(transform.position, _target.position) < attackDistance)
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
    abstract public IEnumerator AttackEffect();

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

            hp = maxHp;

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

        hp -= hitPower;

        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();
        if (hp > 0)
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
            else if (maxHp/5 <= hitPower)
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
        enemy.GetComponent<FSM>().AddExp(exp);
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
