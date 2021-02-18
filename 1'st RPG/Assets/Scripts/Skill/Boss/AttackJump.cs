using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackJump : TouchingSkill
{
    protected override void processCollision(Collider collision)
    {
        
        collision.GetComponent<Rigidbody>().AddForce((collision.transform.position - transform.position).normalized*100);
    }
}
