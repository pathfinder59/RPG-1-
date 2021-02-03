using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using common;
public class WarriorFSM : PlayableFSM
{
    public override IEnumerator AttackEffect()
    {
        yield return new WaitForSeconds(1.0f);
        Target.GetComponent<EnemyFSM>().Damaged(5,gameObject.transform);
    }

    public override IEnumerator LateDie(Transform enemy)
    {
        var fsm = enemy.GetComponent<FSM>();
        if (fsm != null)
            fsm.FindTarget(5, 1 << LayerMask.NameToLayer("Enemy"));
        yield break;
    }
    
    public void Active()
    {
        var go = ParticlePoolManager.Instance.Spawn("WarriorActive");
        go.transform.position = transform.position;
        go.transform.forward = transform.forward;
        go.GetComponent<OneTouchSkill>().Caster = gameObject.transform;
        go.GetComponent<OneTouchSkill>().Atk = Status.SkillLevels["Active"] * 10 + (int)((Status.Atk + AddAtk) * 0.3);
    }

    public void Passive()
    {
        var go = ParticlePoolManager.Instance.Spawn("WarriorPassive");
        go.transform.position = transform.position;
        go.transform.forward = transform.forward;
        go.GetComponent<OneTouchSkill>().Caster = gameObject.transform;
        go.GetComponent<OneTouchSkill>().Atk = Status.SkillLevels["Passive"] * 20 + (int)((Status.Atk + AddAtk) * 0.7);
    }
    public override void AttackEvent()
    {
        Target.GetComponent<EnemyFSM>().Damaged(Status.Atk + AddAtk , gameObject.transform);
    }
}
