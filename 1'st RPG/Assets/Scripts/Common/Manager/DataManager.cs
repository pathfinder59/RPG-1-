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


        public Dictionary<int, List<QuestData>> questDict;   //key : npc id, Value: 해당되는 퀘스트들
        public List<QuestData> questDatabase;

        private void Awake()
        {
            questDict = new Dictionary<int, List<QuestData>>();
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
            
            if (!questDict.ContainsKey(data.client))
                questDict[data.client] = new List<QuestData>();

            questDict[data.client].Add(Instantiate<QuestData>(data));
            if(data.isActive)
                GameObject.Find("Environment").transform.Find("Npcs").Find(data.client.ToString()).GetComponent<Npc>().SetQuestImage(1);
            //위 부분은 현재 맵이 하나만 있다는 가정하에 작성한 코드이기 때문에 이후 맵이 여러개가 된다면, 플레이어 매니저에서
            //플레이어가 위치한 맵이름을 기억하게 해두고 Npcs자식은 맵에 동일하게 존재하니 두 데이터를 통해서 transform을 찾아 호출해주는 함수를 플레이어 매니저에 만들어 두자.
        }
    }
}