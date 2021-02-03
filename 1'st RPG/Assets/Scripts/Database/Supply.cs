using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Supply Data", menuName = "Scriptable Object/Item/SupplyData")]
public class Supply : ItemData
{
    [SerializeField]
    int amount;

    public int Amount => amount;
}
