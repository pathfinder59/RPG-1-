using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{

    Transform target;
    // Start is called before the first frame update
    void Start()
    {
        target = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.LookAt(target);
        transform.forward = target.forward;//new Vector3(-transform.forward.x,0, -transform.forward.z);
        //transform.forward = 0;
    }
}
