using common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BossFSM : EnemyFSM
{

    float accessTime = 0.0f;
    bool isAttacking = false;
    [SerializeField]
    GameObject AttackClawObj;
    [SerializeField]
    GameObject AttackHornObj;
    [SerializeField]
    GameObject AttackJumpObj;
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

    public override void AttackEvent()
    {
        Target.GetComponent<PlayableFSM>().Damaged(5, gameObject.transform);
    }

    public override IEnumerator LateDie(Transform enemy)
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(2);   
    }

    void SpawnSkill(GameObject obj,int hitPower)
    {
        obj.SetActive(true);
        obj.GetComponentInChildren<TouchingSkill>().SetTarget("Player");
        obj.GetComponentInChildren<TouchingSkill>().SetCaster(gameObject.transform);
        obj.GetComponentInChildren<TouchingSkill>().SetAtk(hitPower);
    }
    public void AttackClaw()
    {
        SpawnSkill(AttackClawObj, 100);
    }
    public void AttackJump()
    {
        SpawnSkill(AttackJumpObj, 80);
    }
    public void AttackHorn()
    {
        SpawnSkill(AttackHornObj, 100);
    }
}
