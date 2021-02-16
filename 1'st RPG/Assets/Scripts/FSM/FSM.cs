﻿using common;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSM : MonoBehaviour
{
    protected Transform _target;
    protected float enemyWidth;
    public Transform Target { get { return _target; } set { _target = value; } }

    public void FindTarget(float distance, LayerMask layerMask) 
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, distance, layerMask);
        if (colliders.Length == 0)
        {
            Target = null;
            return;
        }
        Array.Sort<Collider>(colliders,
            (x, y) => (Vector3.Distance(x.transform.position, transform.position) < Vector3.Distance(y.transform.position, transform.position)) ? -1 : 1);
        Target = colliders[0].gameObject.transform;
        enemyWidth = Target.GetComponent<CharacterController>().radius;
    }

    public virtual void AddExp(float exp,GameObject obj = null)
    { }

    public void SpawnDamagedText(int hitPower)
    {
        var go = ParticlePoolManager.Instance.Spawn("PopUpText", transform.position + new Vector3(0, 3, 0));
        go.GetComponentInChildren<TextMesh>().text = "-" + hitPower.ToString();
        go.GetComponentInChildren<TextMesh>().color = new Color(1, 0, 0, 1);
    }
}
