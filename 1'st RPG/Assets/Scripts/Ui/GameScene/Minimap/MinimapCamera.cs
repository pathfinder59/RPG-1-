using common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    Transform playerTrs;
    Camera minimapCam;
    void Start()
    {
        playerTrs = GameObject.Find(PlayerManager.Instance._playerStat.Name).transform;
        minimapCam = transform.GetComponent<Camera>();

        //평상시 size는 90
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTrs.position + new Vector3(0, 50, 0);
    }
}
