﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using common;
public class QuestManager : Singleton<QuestManager>
{
    public Dictionary<int,Quest>[] currentQuests;

    [SerializeField]
    QuestListPage questListPage;
    [SerializeField]
    CurQuestList _curQuestList;
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
        RemoveDescriptor(data);
        RemoveDataFromQuestList(currentQuests, data, clientID);
        SetQuestImage(data.client);

        for (int i = 0 ; i < data.child.Count; ++i)
        {
            int idx = data.childClient[i];
            QuestData child = DataManager.Instance.questDict[idx].Find(_ => _.questIdx == data.child[i]);
            child.nParent--;
            if (child.nParent == 0)
            {
                child.isActive = true;
                SetQuestImage(child.client);
            }
        }
        if(data.CallrutineName.Length != 0)
            ScenemaManager.Instance.CallCorutin(data.CallrutineName);
    }
    public void StartQuest(Dictionary<int, Quest> currentQuests, QuestData data, int clientID)
    {
        AddDataToQuestList(currentQuests, data, clientID);
        SetQuestImage(data.client);
        AddDescriptor(data);
    }
    public void AddDescriptor(QuestData data)
    {
        _curQuestList.AddDescriptor(data);
    }
    public void RemoveDescriptor(QuestData data)
    {
        _curQuestList.RemoveDescriptor(data);
    }

    void AddDataToQuestList(Dictionary<int, Quest> currentQuests, QuestData data, int clientID)
    {
        Dictionary<int, List<QuestData>> questDictionary = DataManager.Instance.questDict;

        if (data._type == '1')
        {
            if (data.target != data.client)
            {
                if (!questDictionary.ContainsKey(data.target))
                    questDictionary[data.target] = new List<QuestData>();

                questDictionary[data.target].Add(data);
            }
            currentQuests.Add(data.questIdx, new Quest(data, true));

            GameObject.Find("Environment").transform.Find("Npcs").Find(data.target.ToString()).GetComponent<Npc>().SetQuestImage(3);
        }
        else
            currentQuests.Add(data.questIdx, new Quest(data));
    }

    void RemoveDataFromQuestList(Dictionary<int, Quest> currentQuests, QuestData data, int clientID)
    {
        Dictionary<int, List<QuestData>> questDictionary = DataManager.Instance.questDict;

        if (data._type == '1')
        {
            if (data.target != clientID)
                return;
            questDictionary[data.target].Remove(data);
            SetQuestImage(data.target);
        }
        questDictionary[data.client].Remove(data);
        currentQuests.Remove(data.questIdx);

        PlayerManager.Instance._playerStat.AddExp(data.exp);
    }
    void SetQuestImage(int client)
    {
        if (DataManager.Instance.questDict[client].Count == 0)
            GameObject.Find(client.ToString()).GetComponent<Npc>().SetImageActive(false);
        else
        {
            int n = 2;

            foreach (QuestData questData in DataManager.Instance.questDict[client])
            {
                if (!questData.isActive)
                    continue;
                if (!QuestManager.Instance.currentQuests[questData._type - '0'].ContainsKey(questData.questIdx)) //수락한 퀘스트가 아닐경우
                {
                    if ((questData._type == '1' && questData.client == client) || questData._type == '0')
                        n = 1;
                }
                else
                {
                    if (QuestManager.Instance.currentQuests[questData._type - '0'][questData.questIdx].processRate == 3)
                    {
                        if (!(questData._type == '1' && questData.client == client))
                        {
                            n = 3;
                            break;
                        }
                        else if(questData.client == questData.target)
                        {
                            n = 3;
                            break;
                        }
                    }
                }
            }
            GameObject.Find("Environment").transform.Find("Npcs").Find(client.ToString()).GetComponent<Npc>().SetQuestImage(n);
        }
    }

    public int GetQuestProceesRate(int id, Dictionary<int, Quest> QuestList, QuestData data)
    {
        int idx;
        if (QuestList.ContainsKey(data.questIdx))
        {
            idx = QuestList[data.questIdx].processRate;
            //idx = data.questIdx - 1 + idx;
            if (data._type == '1' && data.target != id)
            {
                idx = 2; //진행중으로 이동
            }
        }
        else
            idx = 1;
        return idx;
    }

    public void ProcessQuest(int id,int processRate ,QuestData clickedQuestData, Dictionary<int, Quest> questDict)
    {
        QuestContent.clickedContent = null;
        UiController.Instance.OpenDialogPage(false);
        processRate %= 3;
        switch (processRate)
        {
            case 0:  //퀘스트 완료
                ClearQuest(questDict, clickedQuestData, id);
                break;
            case 1: //퀘스트 시작
                StartQuest(questDict, clickedQuestData, id);
                break;
            case 2: //퀘스트 진행중
                break;
        }
        GameSceneManager.Instance.SetIsAct(false);
        GameSceneManager.Instance.SetInterctBtnAct(true);
    }

    public bool OpenQuestList(int id)
    {
        if (QuestContent.clickedContent != null)
            return false;
        else
        {
            UiController.Instance.OpenQuestPage(true);

            Dictionary<int, List<QuestData>> database = DataManager.Instance.questDict;

            foreach (QuestData data in database[id])
            {
                if (data.isActive)
                    questListPage.AddContent(data);
            }

            EventManager.Emit("UpdataQuestPage");
            
            return true;
        }
    }
}
