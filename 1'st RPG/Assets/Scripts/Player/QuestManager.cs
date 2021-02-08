﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using common;
public class QuestManager : MonoBehaviour
{
    public Dictionary<int,Quest>[] currentQuests;
    private void Awake()
    {

        currentQuests = new Dictionary<int, Quest>[3];
        for(int i = 0; i<3; ++i)
            currentQuests[i] = new Dictionary<int, Quest>();
        
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Init()
    {

    }
    //퀘스트 클리어 했을시 무조건 이미지가 삭제되도록 되어있었는데 이제 하나의 캐릭터가 여러 퀘스트를 보유 할 수 있기 때문에 이부분 수정해야함.
    public void ClearQuest(Dictionary<int, Quest> currentQuests, QuestData data,int clientID)
    {
        if (data._type == '1')
        {
            DataManager.Instance.questList[data.target].Remove(data);
            if (data.target != clientID)
                return;
            GameObject.Find(data.target.ToString()).GetComponent<Npc>().SetImageActive(false);
        }
        DataManager.Instance.questList[data.client].Remove(data);
        currentQuests.Remove(data.questIdx + data.client);
        PlayerManager.Instance._playerStat.AddExp(data.exp);
        GameObject.Find(data.client.ToString()).GetComponent<Npc>().SetImageActive(false);

        foreach (int i in data.child)
        {
            int idx = (int)((float)i * 0.001f) * 1000;
            QuestData child = DataManager.Instance.questList[idx].Find(_ => _.questIdx + _.client == i);
            child.nParent--;
            if (child.nParent == 0)
            {
                child.isActive = true;
                GameObject.Find(child.client.ToString()).GetComponent<Npc>().SetQuestImage(1);
            }
        }

    }
    public void StartQuest(Dictionary<int, Quest> currentQuests, QuestData data, int clientID)
    {
        if (data._type == '1')
        {
            if (!DataManager.Instance.questList.ContainsKey(data.target))
                DataManager.Instance.questList[data.target] = new List<QuestData>();
            DataManager.Instance.questList[data.target].Insert(0, data);
            currentQuests.Add(data.questIdx + data.client, new Quest(data, true));
            GameObject.Find(data.target.ToString()).GetComponent<Npc>().SetQuestImage(3);
        }
        else
            currentQuests.Add(data.questIdx + data.client, new Quest(data));
        GameObject.Find(data.client.ToString()).GetComponent<Npc>().SetQuestImage(2);
    }
}
