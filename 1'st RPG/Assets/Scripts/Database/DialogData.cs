using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog Data", menuName = "Scriptable Object/DialogData")]
public class DialogData : ScriptableObject
{
    public int _id;  //모든 npc오브젝트는 아이디를 1000단위로 증가하면서 가질거임, 나머지 100단위 이하는 가질 수 있는 대화모음이라고 할 수 있음.
    //항상 0번째는 default 대화문, 이후에는 세개씩 끊어서 퀘스트의 수락시,진행중,완료시의 대화문을 갖도록 할 예정
    //ex id+0 = default, id+1 = quest1's start, id+2 = quest2's ready, id+3 = quest3's finish

    public List<string> _dialogs; 

    
}