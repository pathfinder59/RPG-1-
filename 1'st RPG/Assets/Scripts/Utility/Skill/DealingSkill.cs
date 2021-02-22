using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DealingSkill : Skill
{
    protected bool isOn;
    private void OnEnable()
    {
        isOn = true;
        StartCoroutine("Act");
    }
    protected abstract void processCollision(Collider collision);

}
