using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Utillity;
public class AttackButton : MonoBehaviour
{
    PlayerFSM _playerFsm;
    void Start()
    {
        _playerFsm = GameObject.Find("Player").GetComponent<PlayerFSM>();
    }

    void Update()
    {
        
    }

    public void OnClickBtn()
    {
        if (_playerFsm.target == null)
        {
            Collider[] colliders = Physics.OverlapSphere(_playerFsm.gameObject.transform.position, 10f);
            if (colliders.Length == 0)
                return;
            // Algorithm.SortColliderDistance(colliders, 0, colliders.Length - 1, _playerFsm.gameObject);
            _playerFsm.target = colliders[1].gameObject;
        }
        else
            _playerFsm.target = null;

    }
}
