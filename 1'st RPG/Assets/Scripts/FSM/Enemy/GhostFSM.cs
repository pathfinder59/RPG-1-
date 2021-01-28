using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFSM : EnemyFSM
{
    public override IEnumerator AttackEffect()
    {
        yield break;
    }


    public override IEnumerator LateDie(Transform enemy)
    {
        gameObject.layer = LayerMask.NameToLayer("Die");
        var fsm = enemy.GetComponent<FSM>();
        if(fsm != null)
            fsm.FindTarget(5, 1 << LayerMask.NameToLayer("Enemy"));

        yield break;
    }
}
