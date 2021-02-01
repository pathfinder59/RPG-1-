using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FSM : MonoBehaviour
{
    protected Transform _target;
    public Transform Target { get { return _target; } set { _target = value; } }

    public void FindTarget(float distance, LayerMask layerMask) 
    {
        Collider[] colliders = Physics.OverlapSphere(gameObject.transform.position, 10f, layerMask);
        if (colliders.Length == 0)
        {
            Target = null;
            return;
        }
        Array.Sort<Collider>(colliders,
            (x, y) => (Vector3.Distance(x.transform.position, transform.position) < Vector3.Distance(y.transform.position, transform.position)) ? -1 : 1);
        Target = colliders[0].gameObject.transform;
    }

    public virtual void AddExp(float exp)
    { }

}
