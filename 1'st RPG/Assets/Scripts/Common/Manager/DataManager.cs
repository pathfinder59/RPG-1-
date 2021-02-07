using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GameScene.Skill;


namespace common
{
    public class DataManager : Singleton<DataManager>
    {
        public List<SkillDatabase> skillDatabases;
        public List<StatusData> statusDatabase;
        public List<DialogData> dialogDatabase;


        public Dictionary<int, List<QuestData>> questList;   //key : npc id, Value: 해당되는 퀘스트들
        public List<QuestData> questDatabase;

        private void Awake()
        {
            questList = new Dictionary<int, List<QuestData>>();
        }
        void Start()
        {
            //처음엔 다 불러오지 않고 활성화된 퀘스트만 리스트에 넣도록 한다.
            // 그런 다음에 퀘스트가 클리어 될때마다
            foreach (QuestData data in DataManager.Instance.questDatabase)
            {
                AddQuestData(data);
            }
        }

        void Update()
        {
            
        }

        public void AddQuestData(QuestData data)
        {
            
            if (!questList.ContainsKey(data.client))
                questList[data.client] = new List<QuestData>();
            
            questList[data.client].Add(Instantiate<QuestData>(data));
            if(data.isActive)
                GameObject.Find(data.client.ToString()).GetComponent<Npc>().SetQuestImage(1);
        }
    }
}