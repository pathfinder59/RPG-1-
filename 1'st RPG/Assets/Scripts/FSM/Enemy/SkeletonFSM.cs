using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonFSM : EnemyFSM
{

    public override void AttackEvent()
    {
        Target.GetComponent<PlayableFSM>().Damaged(_stat.Atk, gameObject.transform);
    }

    public override IEnumerator LateDie(Transform enemy)
    {
        yield break;
    }
}
