using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace common
{
    using GameScene.Skill;
    public class PlayerManager : Singleton<PlayerManager>
    {

        public SkillData skillData;

        public PlayerStatus _playerStatus;

        [SerializeField]
        GameObject prefab;
        
        private void Awake()
        {
  
            _playerStatus = new PlayerStatus();

            _playerStatus.Hp = 150;
            _playerStatus.MaxHp = 150;
            _playerStatus.Level = 1;
            _playerStatus.Name = "Player";


            _playerStatus.ClassType = "Warrior"; // 임시로 클래스는 자동으로 워리어를 갖도록 함 이후
                                                 // 캐릭터 추가까지 완성된다면 이부분은 바뀔것임

            _playerStatus.SkillPoint = 3;


            _playerStatus.Exp = 0;
            _playerStatus.MaxExp = 100;

            _playerStatus.SetSkillLevels();

            Instantiate(prefab).name = "Player"; // 캐릭터의 클래스타입에 따라 캐릭터 모델 결정
        }
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}