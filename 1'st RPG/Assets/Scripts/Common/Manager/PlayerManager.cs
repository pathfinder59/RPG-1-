using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace common
{
    using GameScene.Skill;
    public class PlayerManager : Singleton<PlayerManager>
    {
        public int AddtiveAtk { get; set; }
        public int AddtiveDef { get; set; }
        int _addtiveDef;


        public SkillData skillData;

        public PlayerStat _playerStat;

        [SerializeField]
        EquipmentPage _equipmentPage;

        public int Money{get;set;}
        public int NumHp { get; set; }
        private void Awake()
        {
            AddtiveAtk = 0;
            AddtiveDef = 0;

            NumHp = 0;
            Money = 100000;
            var go = ObjectPoolManager.Instance.Spawn(GameManager.Instance.playerClass);
            go.AddComponent<PlayerMove>();
            //go.transform.position = GameObject.Find("Map").transform.Find("PlayerSpawn").position;
            //여기서 플레이어 컨트롤러 획득을 위한 playermove컴포넌트가 go에 추가될 것임.
            //여기서 플레이어의 위치도 업데이트할 필요가 있음.
            _playerStat = go.GetComponentInParent<PlayerStat>();

            _playerStat.Hp = 150;
            _playerStat.MaxHp = 150;
            _playerStat.Level = 1;
            _playerStat.Name = "Player";


            _playerStat.ClassType = "Warrior"; // 임시로 클래스는 자동으로 워리어를 갖도록 함 이후
                                                 // 캐릭터 추가까지 완성된다면 이부분은 바뀔것임

            _playerStat.SkillPoint = 3;


            _playerStat.Exp = 0;
            _playerStat.MaxExp = 100;

            _playerStat.SetSkillLevels();

            go.name = "Player"; // 캐릭터의 클래스타입에 따라 캐릭터 모델 결정
        }
        // Start is called before the first frame update
        void Start()
        {
            EventManager.On("UpdatePlayerEquip", UpdatePlayerEquipment);
        }

        // Update is called once per frame
        void Update()
        {

        }

        void UpdatePlayerEquipment(GameObject obj = null)
        {
            AddtiveAtk = 0;
            AddtiveDef = 0;
            foreach(EquipBtn btn in _equipmentPage.EquipUis)
            {
                Equipment data = btn.Data as Equipment;
                if (data == null)
                    continue;
                AddtiveAtk += data.Atk;
                AddtiveDef += data.Def;
            }
            _playerStat.transform.GetComponent<PlayableFSM>().AddAtk = AddtiveAtk;
            _playerStat.transform.GetComponent<PlayableFSM>().AddDef = AddtiveDef;
        }
    }
}