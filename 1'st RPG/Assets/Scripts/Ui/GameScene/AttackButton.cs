﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utillity;
public class AttackButton : MonoBehaviour
{
    PlayableFSM _playerFsm;
    void Start()
    {
        _playerFsm = GameObject.Find("Player").GetComponent<PlayableFSM>();
    }

    void Update()
    {
        
    }

    public void OnClickBtn()
    {
        if (_playerFsm.Target == null)
            _playerFsm.FindTarget(10f, 1 << LayerMask.NameToLayer("Enemy"));
        else
            _playerFsm.Target = null;

    }
}
