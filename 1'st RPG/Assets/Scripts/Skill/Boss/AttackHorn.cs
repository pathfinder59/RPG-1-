using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHorn : TouchingSkill
{
    protected override void processCollision(Collider collision)
    {
        collision.GetComponent<Rigidbody>().AddForce(transform.up * 50);
    }

}
