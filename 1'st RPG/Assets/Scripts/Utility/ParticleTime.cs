using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTime : MonoBehaviour
{
    [SerializeField]
    float time;

    Transform target;

    void OnEnable()
    {
        StartCoroutine("On");
    }

    public void SetTarget(Transform obj)
    {
        target = obj;
    }
    void Update()
    {
        if(target != null)
        {
            gameObject.transform.position = target.position;
        }
    }
    IEnumerator On()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
