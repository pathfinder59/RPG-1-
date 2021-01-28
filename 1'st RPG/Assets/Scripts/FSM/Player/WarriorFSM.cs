using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    
    public IEnumerator Active()
    {
        yield return null;
    }

    public IEnumerator Passive()
    {
        yield return null;
    }

}
