using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace common
{
    using GameScene.Skill;

    public class PlayerManager : Singleton<PlayerManager>
    {
        public PlayerStat _playerStat;

        [SerializeField]
        EquipmentPage _equipmentPage;
        [SerializeField]
        Transform spawnRegion;

        public int Money{get;set;}
        public int NumHp { get; set; } //포션 개수

        
        private void Awake()
        {
            
            NumHp = 0;
            Money = 100000;
            Init();
            
        }
        void Start()
        {
            EventManager.On("UpdatePlayerEquip", UpdatePlayerEquipment);

        }

        void Update()
        {

        }

        public void IncreasePotion()
        {
            NumHp++;
            EventManager.Emit("UpdatePotion");
        }
        void Init()
        {
            var go = ObjectPoolManager.Instance.Spawn(GameSceneManager.Instance.playerClass, spawnRegion.position, spawnRegion.rotation);
            go.transform.position = spawnRegion.position;
            go.AddComponent<PlayerMove>();
            //go.AddComponent<QuestManager>();
            _playerStat = go.GetComponentInParent<PlayerStat>();

            _playerStat.Hp = 150;
            _playerStat.MaxHp = 150;
            _playerStat.Level = 1;
            _playerStat.Name = "Player";


            _playerStat.ClassType = "Warrior"; // 임시로 클래스는 자동으로 워리어를 갖도록 함 이후
                                               // 캐릭터 추가까지 완성된다면 이부분은 바뀔것임

            _playerStat.SkillPoint = 0;


            _playerStat.Exp = 0;
            _playerStat.MaxExp = 100;

            _playerStat.SetSkillLevels();

            go.name = "Player"; // 캐릭터의 클래스타입에 따라 캐릭터 모델 결정
        }
        void UpdatePlayerEquipment(GameObject obj = null)
        {
            int AddtiveAtk = 0;
            int AddtiveDef = 0;
            foreach(EquipSlot slot in _equipmentPage.EquipSlots)
            {
                if (slot.transform.childCount == 0)
                    continue;
                Equipment data = slot.GetComponentInChildren<ItemUi>().Data as Equipment ?? null;
                if (data == null)
                    continue;
                AddtiveAtk += data.Atk;
                AddtiveDef += data.Def;
            }
            _playerStat.GetComponent<PlayableFSM>().AddAtk = AddtiveAtk;
            _playerStat.GetComponent<PlayableFSM>().AddDef = AddtiveDef;
        }
    }
}