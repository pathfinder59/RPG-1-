using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using common;
public class QuestManager : Singleton<QuestManager>
{
    public Dictionary<int,Quest>[] currentQuests;
    [SerializeField]
    Transform curQuestContent;

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

        foreach (int i in data.child)
        {
            int idx = (int)((float)i * 0.001f) * 1000;
            QuestData child = DataManager.Instance.questDict[idx].Find(_ => _.questIdx + _.client == i);
            child.nParent--;
            if (child.nParent == 0)
            {
                child.isActive = true;
                SetQuestImage(child.client);
            }
        }
    }
    public void StartQuest(Dictionary<int, Quest> currentQuests, QuestData data, int clientID)
    {
        AddDataToQuestList(currentQuests, data, clientID);
        SetQuestImage(data.client);
        AddDescriptor(data);
    }
    public void AddDescriptor(QuestData data)
    {
        QuestDescriptor descriptor = ObjectPoolManager.Instance.Spawn("QuestDescriptor").GetComponent<QuestDescriptor>();
        descriptor.SetData(data);
        descriptor.transform.SetParent(curQuestContent);
        EventManager.Emit("UpdateDescriptor");
    }
    public void RemoveDescriptor(QuestData data)
    {
        for(int i = 0;i < curQuestContent.childCount;++i)
        {
            if (curQuestContent.GetChild(i).GetComponent<QuestDescriptor>().Data == data)
            { 
                curQuestContent.GetChild(i).gameObject.SetActive(false);
                return;
            }
        }
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
            currentQuests.Add(data.questIdx + data.client, new Quest(data, true));

            GameObject.Find("Environment").transform.Find("Npcs").Find(data.target.ToString()).GetComponent<Npc>().SetQuestImage(3);
        }
        else
            currentQuests.Add(data.questIdx + data.client, new Quest(data));
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
        currentQuests.Remove(data.questIdx + data.client);

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
                if (!QuestManager.Instance.currentQuests[questData._type - '0'].ContainsKey(questData.questIdx + client))
                {
                    if ((questData._type == '1' && questData.target != client) || questData._type == '0')
                        n = 1;
                }
                else
                {
                    if (QuestManager.Instance.currentQuests[questData._type - '0'][questData.questIdx + client].processRate == 3)
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
}
