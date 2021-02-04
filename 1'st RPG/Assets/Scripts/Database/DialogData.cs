using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog Data", menuName = "Scriptable Object/DialogData")]
public class DialogData : ScriptableObject
{
    public int _id;
    public List<string> _dialogs;
}