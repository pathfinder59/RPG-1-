using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class NonPlayableFSM : MonoBehaviour
{

    public enum EnemyState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged,
        Die
    }
    EnemyState m_State;

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
    int hitPoint;  //이 수치 이상으로 데미지를 받을 경우 피격모션 발생

    [SerializeField]
    float hitTime;
    [SerializeField]
    float dieTime;

    Animator _anim;
    NavMeshAgent _navMeshAgent;

    void Start()
    {
        m_State = EnemyState.Idle;
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

        switch (m_State)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Move:
                Move();
                break;
            case EnemyState.Attack:
                Attack();
                break;
            case EnemyState.Return:
                Return();
                break;
            case EnemyState.Damaged:
                //Damaged();
                break;
            case EnemyState.Die:
                //Die();
                break;
        }

        //hpSlider.value = (float)hp / (float)maxHp;
    }

    void Idle()
    {
        if (_target != null)
        {
            m_State = EnemyState.Move;
            print("상태 전환: Idle -> Move");

            _anim.SetTrigger("IdleToMove");
        }
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, _target.position) > moveDistance)
        {
            m_State = EnemyState.Return;
            print("상태 전환: Move -> Return");
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

            m_State = EnemyState.Attack;
            print("상태 전환: Move -> Attack");

            _anim.SetTrigger("MoveToAttackDelay");           
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
            m_State = EnemyState.Move;
            print("상태 전환: Attack -> Move");
            _anim.SetTrigger("AttackToMove");
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

            m_State = EnemyState.Idle;
            print("상태 전환: Return -> Idle");

            _anim.SetTrigger("MoveToIdle");
        }
    }

    public void HitEnemy(int hitPower)
    {
        if (m_State == EnemyState.Damaged || m_State == EnemyState.Die || m_State == EnemyState.Return)
        {
            return;
        }

        hp -= hitPower;

        _navMeshAgent.isStopped = true;
        _navMeshAgent.ResetPath();
        if (hp > 0)
        {
            if (hitPoint <= hitPower)
            {
                m_State = EnemyState.Damaged;
                print("상태 전환: Any State -> Damaged");
                _anim.SetTrigger("Damaged");
                Damaged();
            }
        }
        else
        {
            m_State = EnemyState.Die;
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

        m_State = EnemyState.Move;
        print("상태 전환: Damaged -> Move");
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
