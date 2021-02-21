using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest Data", menuName = "Scriptable Object/QuestData")]
public class QuestData : ScriptableObject
{
    public int questIdx; //대화 번호
    public string title;
    public int client; //퀘스트를 주는 npc
    public int target;

    public char _type; // 0:토벌, 1:대화, 2:재료
    public int num; //갯수 1경우에는 제외
    public int exp; //보상 경험치
    
    
    public List<int> child;
    public List<int> childClient;
    public int nParent;


    public string descript;
    public bool isActive;

    public List<DialogData> _dialogs;
    public string CallrutineName = null;
    public QuestData(QuestData data)
    {
        questIdx = data.questIdx;
        title = data.title;
        client = data.client;
        target = data.target;
        _type = data._type;
        num = data.num;
        exp = data.exp;

        

        child = new List<int>();
        child = data.child.GetRange(0, data.child.Count);
        childClient = new List<int>();
        childClient = data.childClient.GetRange(0, data.childClient.Count);

        nParent = data.nParent;
        isActive = data.isActive;

        _dialogs = new List<DialogData>();
        _dialogs = data._dialogs.GetRange(0, data._dialogs.Count);
        CallrutineName = data.CallrutineName;
    }
}

