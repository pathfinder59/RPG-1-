using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegularSkill : DealingSkill
{
    [SerializeField]
    float _time;

    void OnDisable()
    {
        StopAllCoroutines();
    }
    protected override void processCollision(Collider collision) { }

    void OnCollisionStay(Collision collision)
    {
        OnTriggerStay(collision.collider);
    }
    void OnTriggerStay(Collider other)
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
        while (true)
        {
            yield return new WaitForFixedUpdate();
            isOn = false;
            yield return new WaitForSeconds(_time);
            isOn = true;
        }
    }
}
