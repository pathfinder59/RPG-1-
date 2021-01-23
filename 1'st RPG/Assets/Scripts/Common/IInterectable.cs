using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInterectable
{
    void Damaged(int amount);
    void Heal(int amount);
}
