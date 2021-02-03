using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using common;
using System;

[CreateAssetMenu(fileName = "StatusData", menuName = "Scriptable Object/StatusData")]
public class StatusData : ScriptableObject
{
    [Serializable]
    public struct initialStatus
    {
        [SerializeField]
        int _maxHp;
        [SerializeField]
        int _maxExp;
        [SerializeField]
        int _atk;
        [SerializeField]
        float _moveSpeed;

        public int MaxHp => _maxHp;
        public int MaxExp => _maxExp;
        public int Atk => _atk;
        public float MoveSpeed => _moveSpeed;
    }
    
    [SerializeField]
    string className;
    [SerializeField]
    initialStatus initialStatusData;
}
