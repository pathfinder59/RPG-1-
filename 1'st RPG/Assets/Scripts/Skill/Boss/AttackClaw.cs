using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackClaw : TouchingSkill
{
    protected override void processCollision(Collider collision)
    {
        collision.GetComponent<Rigidbody>().AddForce(transform.forward*50);
    }
}
