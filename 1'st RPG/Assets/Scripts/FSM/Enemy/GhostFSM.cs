using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFSM : NonPlayableFSM
{
    public override IEnumerator AttackEffect()
    {
        yield break;
    }

    public override IEnumerator DieEffect()
    {
        yield break;
    }
}
