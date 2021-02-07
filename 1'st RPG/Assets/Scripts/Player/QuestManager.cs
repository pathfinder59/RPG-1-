using System.Collections;
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

    public void ClearQuest(Dictionary<int, Quest> currentQuests, QuestData data,int clientID)
    {
        if (data._type == '1')
        {
            DataManager.Instance.questList[data.target].Remove(data);
            if (data.target != clientID)
                return;
        }
        DataManager.Instance.questList[data.client].Remove(data);
        currentQuests.Remove(data.questIdx + data.client);
        PlayerManager.Instance._playerStat.AddExp(data.exp);

        foreach(int i in data.child)
        {
            int idx = (int)((float)i * 0.001f) * 1000;
            QuestData child = DataManager.Instance.questList[idx].Find(_ => _.questIdx + _.client == i);
            child.nParent--;
            if (child.nParent == 0)
                child.isActive = true;
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
        }
        else
            currentQuests.Add(data.questIdx + data.client, new Quest(data));
    }
}
