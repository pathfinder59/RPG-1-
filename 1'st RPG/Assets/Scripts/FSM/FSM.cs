using common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class FSM : MonoBehaviour
{
    protected Transform _target;
    protected float enemyWidth;
    protected FuncState _state;
    protected float currentTime = 0;
    protected NavMeshAgent _navMeshAgent;
    protected Animator _animator;
    [SerializeField]
    protected float attackDelay;  //공격 딜레이 길이
    [SerializeField]
    protected float attackDistance; //공격 범위
    public Transform Target { get { return _target; } set { _target = value; } }

    public void FindTarget(float distance, LayerMask layerMask) 
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, distance, layerMask);
        if (colliders.Length == 0)
        {
            Target = null;
            return;
        }
        Array.Sort<Collider>(colliders,
            (x, y) => (Vector3.Distance(x.transform.position, transform.position) < Vector3.Distance(y.transform.position, transform.position)) ? -1 : 1);
        Target = colliders[0].gameObject.transform;
        enemyWidth = Target.GetComponent<CharacterController>().radius;
    }

    public virtual void AddExp(float exp,GameObject obj = null)
    { }

    public void SpawnDamagedText(int hitPower)
    {
        var go = ParticlePoolManager.Instance.Spawn("PopUpText", transform.position + new Vector3(0, 3, 0));
        go.GetComponentInChildren<TextMesh>().text = "-" + hitPower.ToString();
        go.GetComponentInChildren<TextMesh>().color = new Color(1, 0, 0, 1);
    }

    public enum FuncState
    {
        Idle,
        Move,
        Attack,
        Return,
        Damaged, 
        Die
    }


    public void UpdataRoutine()
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
        }
    }
    public virtual void Idle() 
    {
        if (_target != null)
        {
            _state = FuncState.Move;
            _animator.SetTrigger("Move");
        }
    }
    public virtual void Move() { }
    public virtual void Attack() 
    {
        if (Vector3.Distance(transform.position, _target.position) <= attackDistance + enemyWidth)
        {
            if (currentTime == 0)
            {
                transform.LookAt(_target);
                currentTime = attackDelay;
                _animator.SetTrigger("StartAttack");
            }
        }
        else
        {
            _state = FuncState.Move;
            _animator.SetTrigger("Move");
        }
    }
    public virtual void Return() { }

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
