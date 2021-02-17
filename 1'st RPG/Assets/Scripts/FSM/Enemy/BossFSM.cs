﻿using common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFSM : EnemyFSM
{

    float accessTime = 0.0f;
    bool isAttacking = false;
    [SerializeField]
    GameObject AttackClawObj;
    [SerializeField]
    GameObject AttackHornObj;
    public override void Attack()
    {
        if (Vector3.Distance(transform.position, _target.position) < attackDistance)
        {
            accessTime += Time.deltaTime;

            if (currentTime == 0)
            {
                transform.LookAt(_target);
                currentTime = attackDelay;
                if (accessTime >= 10)
                {
                    accessTime = 0;
                    isAttacking = true;
                    int value = Random.Range(1, 5);
                    if(value >= 4)
                        _animator.SetTrigger("Jump");
                    else
                        _animator.SetTrigger("Claw");
                }
                else
                {
                    if (Hp < MaxHp / 2)
                    {
                        isAttacking = true;
                        _animator.SetTrigger("Horn");
                    }
                    else
                        _animator.SetTrigger("StartAttack");
                }
            }
        }
        else
        {
            accessTime = 0.0f;
            if (isAttacking)
                return;
            _state = FuncState.Move;
            print("상태 전환: Attack -> Move");
            _animator.SetTrigger("Move");
        }
    }

    public void TurnAttaking()
    {
        isAttacking = !isAttacking;
    }

    public void AttackClaw()
    {
        AttackClawObj.SetActive(true);
        AttackClawObj.transform.position = transform.position;
        AttackClawObj.transform.forward = transform.forward;
        AttackClawObj.GetComponentInChildren<OneTouchSkill>().target = "Player";
        AttackClawObj.GetComponentInChildren<OneTouchSkill>().Caster = gameObject.transform;
        AttackClawObj.GetComponentInChildren<OneTouchSkill>().Atk = 150;
    }
    public void AttackJump()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 10, 1 << LayerMask.NameToLayer("Player"));
        foreach(Collider collider in colliders)
        {
            collider.gameObject.GetComponent<PlayableFSM>().Damaged(80, gameObject.transform);
        }
    }
    public void AttackHorn()
    {
        AttackHornObj.SetActive(true);
        AttackHornObj.transform.position = transform.position;
        AttackHornObj.transform.forward = transform.forward;
        AttackHornObj.GetComponentInChildren<OneTouchSkill>().target = "Player";
        AttackHornObj.GetComponentInChildren<OneTouchSkill>().Caster = gameObject.transform;
        AttackHornObj.GetComponentInChildren<OneTouchSkill>().Atk = 100;
    }
    public override void AttackEvent()
    {
        Target.GetComponent<PlayableFSM>().Damaged(5, gameObject.transform);
    }

    public override IEnumerator LateDie(Transform enemy)
    {
        yield break;
    }
}
