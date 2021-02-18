using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using common;
public class WarriorFSM : PlayableFSM
{
    [SerializeField]
    GameObject ActiveEfx;
    [SerializeField]
    GameObject PassiveEfx;
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
        ActiveEfx.SetActive(true);
   
        ActiveEfx.GetComponent<TouchingSkill>().SetTarget("Enemy");
        ActiveEfx.GetComponent<TouchingSkill>().SetCaster(gameObject.transform);
        ActiveEfx.GetComponent<TouchingSkill>().SetAtk(Status.SkillLevels["Active"] * 15 + (int)((Status.Atk + AddAtk) * 0.6));
    }

    public void Passive()
    {
        PassiveEfx.SetActive(true);
        PassiveEfx.GetComponent<RegularSkill>().SetTarget("Enemy");
        PassiveEfx.GetComponent<RegularSkill>().SetCaster(gameObject.transform);
        PassiveEfx.GetComponent<RegularSkill>().SetAtk(Status.SkillLevels["Passive"] * 10 + (int)((Status.Atk + AddAtk) * 0.3));
    }
    public override void AttackEvent()
    {
        Target.GetComponent<EnemyFSM>().Damaged(Status.Atk + AddAtk , gameObject.transform);
    }
}
