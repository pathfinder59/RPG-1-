using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class NonPlayableFSM : MonoBehaviour
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

    public int attackPower = 3;

    CharacterController cc;

    Transform _target;

    float currentTime = 0;
    [SerializeField]
    float attackDelay;

    Vector3 _originPos;
    Quaternion _originRot;



    int hp;
    public int maxHp;
    [SerializeField]
    int criticalHit;  //이 수치 이상으로 데미지를 받을 경우 피격모션 발생

    [SerializeField]
    float hitTime;
    [SerializeField]
    float dieTime;

    Animator _anim;
    NavMeshAgent _navMeshAgent;

    void Start()
    {
        _state = FuncState.Idle;
        hp = maxHp;

        _target = GameObject.Find("Player").transform;  //변경할것임

        cc = GetComponent<CharacterController>();

        _originPos = transform.position;
        _originRot = transform.rotation;

        _anim = transform.GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
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
            case FuncState.Attack:
                Attack();
                break;
            case FuncState.Return:
                Return();
                break;
            case FuncState.Damaged:
                //Damaged();
                break;
            case FuncState.Die:
                //Die();
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

            _anim.SetTrigger("Move");
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

            _anim.SetTrigger("Attack");           
        }
    }

    void Attack()
    {
        if (Vector3.Distance(transform.position, _target.position) < attackDistance)
        {           
            if (currentTime == 0)
            {
                //player.GetComponent<PlayerMove>().DamageAction(attackPower);
                print("공격");
                currentTime = attackDelay;
                _anim.SetTrigger("StartAttack");
                StartCoroutine("AttackEffect");
            }
        }
        else
        {
            _state = FuncState.Move;
            print("상태 전환: Attack -> Move");
            _anim.SetTrigger("Move");
        }
    }

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

            _anim.SetTrigger("Idle");
        }
    }

    public void HitEnemy(int hitPower,Transform enemy) // 이부분에 매개변수로 데미지를 주는 객체를 넣도록 해서 쫒아가도록 하자(_target에 대입)
    {
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
                    _anim.SetTrigger("Move");
                }
            }
            else if (criticalHit <= hitPower)
            {
                _state = FuncState.Damaged;
                print("상태 전환: Any State -> Damaged");
                _anim.SetTrigger("Damaged");
                Damaged();
            }
        }
        else
        {
            _state = FuncState.Die;
            print("상태 전환: Any State -> Die");
            _anim.SetTrigger("Die");
            Die();
        }
    }

    void Damaged()
    {
        StartCoroutine(DamageProcess());
    }

    IEnumerator DamageProcess()
    {
        yield return new WaitForSeconds(hitTime);

        _state = FuncState.Move;
        print("상태 전환: Damaged -> Move");
        //데미지 애니메이션이 실행될 경우 끝나면 바로 무브로 넘어가기때문에 트리거를 따로 설정하지 않아도 됨
        //이 부분을 바꿀 필요가 잇음
    }
    IEnumerator DieProcess()
    {
        
        yield return new WaitForSeconds(dieTime);
        print("소멸!");
        gameObject.SetActive(false);
    }
    void Die()
    {
        StopAllCoroutines();
        StartCoroutine("DieEffect");
        StartCoroutine(DieProcess());
    }

    abstract public IEnumerator DieEffect();
}
