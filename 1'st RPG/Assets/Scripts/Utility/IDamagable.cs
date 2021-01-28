using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    int Hp { get; set; }
    int MaxHp { get; set; }
    void Damaged(int hitPower, Transform enemy);
}
