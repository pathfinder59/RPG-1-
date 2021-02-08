using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Data", menuName = "Scriptable Object/QuestData")]
public class QuestData : ScriptableObject
{
    public int questIdx; //대화 번호
    public int client; //퀘스트를 주는 npc
    public int target;

    public char _type; // 0:토벌, 1:대화, 2:재료
    public int num; //갯수 1경우에는 제외
    public int exp; //보상 경험치
    //public int[] npcId;   //대화 순서 루틴
    public List<int> child;
    public int nParent;


    public string descript;
    public bool isActive;
    public QuestData(QuestData data)
    {
        questIdx = data.questIdx;
        client = data.client;
        target = data.target;
        _type = data._type;
        num = data.num;
        exp = data.exp;

        child = new List<int>();
        child = data.child.GetRange(0, data.child.Count);

        nParent = data.nParent;
        isActive = data.isActive;
    }
}

