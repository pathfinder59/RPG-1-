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
    float findDistance = 8f;   //탐색 범위
    [SerializeField]
    float moveDistance = 20f;  //추적 범위
    [SerializeField]
    float attackDistance = 2f; //공격 범위

    public int attackPower = 3;

    CharacterController cc;

    Transform player;

    float currentTime = 0;
    [SerializeField]
    float attackDelay = 2f;

    Vector3 _originPos;
    Quaternion _originRot;



    int hp = 15;
    public int maxHp = 15;


    Animator _anim;
    NavMeshAgent _navMeshAgent;

    void Start()
    {
        m_State = EnemyState.Idle;

        player = GameObject.Find("Player").transform;  //변경할것임

        cc = GetComponent<CharacterController>();

        _originPos = transform.position;
        _originRot = transform.rotation;

        _anim = transform.GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
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
        if (Vector3.Distance(transform.position, player.position) < findDistance)
        {
            m_State = EnemyState.Move;
            print("상태 전환: Idle -> Move");

            _anim.SetTrigger("IdleToMove");
        }
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, player.position) > moveDistance)
        {
            m_State = EnemyState.Return;
            print("상태 전환: Move -> Return");
        }
        else if (Vector3.Distance(transform.position, player.position) > attackDistance)
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.ResetPath();
            _navMeshAgent.stoppingDistance = attackDistance;

            _navMeshAgent.destination = player.position;
        }
        else
        {

            m_State = EnemyState.Attack;
            print("상태 전환: Move -> Attack");

            _anim.SetTrigger("MoveToAttackDelay");
            currentTime = attackDelay;
        }
    }

    void Attack()
    {
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            currentTime += Time.deltaTime;
            if (currentTime > attackDelay)
            {
                //player.GetComponent<PlayerMove>().DamageAction(attackPower);
                print("공격");
                currentTime = 0;
                _anim.SetTrigger("StartAttack");
            }

        }
        else
        {
            m_State = EnemyState.Move;
            print("상태 전환: Attack -> Move");
            currentTime = 0;

            _anim.SetTrigger("AttackToMove");
        }
    }
    public void AttackAction()
    {
       // player.GetComponent<PlayerMove>().DamageAction(attackPower);
    }
    void Return()
    {
        if (Vector3.Distance(transform.position, _originPos) > 0.1f)
        {
            //Vector3 dir = (originPos - transform.position).normalized;
            //
            //cc.Move(dir * moveSpeed * Time.deltaTime);
            //
            //transform.forward = dir;

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
            m_State = EnemyState.Damaged;
            print("상태 전환: Any State -> Damaged");
            _anim.SetTrigger("Damaged");
            Damaged();
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
        yield return new WaitForSeconds(1.0f);

        m_State = EnemyState.Move;
        print("상태 전환: Damaged -> Move");
    }
    IEnumerator DieProcess()
    {
        cc.enabled = false;
        yield return new WaitForSeconds(2f);
        print("소멸!");
        Destroy(gameObject);
    }
    void Die()
    {
        StopAllCoroutines();

        StartCoroutine(DieProcess());
    }
}
