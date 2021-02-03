using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Equipment Data", menuName = "Scriptable Object/Item/EquipmentData")]
public class Equipment : ItemData
{
    [SerializeField]
    int _atk;
    [SerializeField]
    int _def;
    public int Atk => _atk;
    public int Def => _def;
}
