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
        yield break;
    }
}
