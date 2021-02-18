using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingSkill : DealingSkill
{
    protected override void processCollision(Collider collision) { }

    void OnCollisionEnter(Collision collision)
    {
        OnTriggerEnter(collision.collider);
    }
    void OnTriggerEnter(Collider other)
    {
        if (!isOn)
            return;

        if (other.gameObject.layer == LayerMask.NameToLayer(_target))
        {
            if (_target == "Player")
                other.gameObject.GetComponentInChildren<PlayableFSM>().Damaged(_atk, _caster);
            else if (_target == "Enemy")
                other.gameObject.GetComponentInChildren<EnemyFSM>().Damaged(_atk, _caster);
            processCollision(other);
        }
    }

    IEnumerator Act()
    {
        yield break;
    }
}
