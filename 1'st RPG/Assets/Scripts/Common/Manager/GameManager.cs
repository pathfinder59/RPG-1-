using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene.Skill;
using GameScene.Ui;
namespace common
{
    public class GameManager : Singleton<GameManager>
    {
        //씬이 변경되어도 이 객체는 항상 값을 유지할 필요가 있음 
        [SerializeField]
        InventoryPage inventoryPage;
        [SerializeField]
        EquipmentPage equipmentPage;
        [SerializeField]
        StatusPage statusPage;
        public string playerClass;

        public InventoryPage Inventory => inventoryPage;

        float spawnTime;
        enum GameState
        {
            Init,Loading,Play
        }

        GameState _gameState;

     
        void Start()
        {
            spawnTime = 5.0f;
            playerClass = "Warrior"; // 완성시에는 이 값은 캐릭터 선택화면에서 결정할것
            _gameState = GameState.Play;

            inventoryPage.gameObject.SetActive(true);
            inventoryPage.gameObject.SetActive(false);
            equipmentPage.gameObject.SetActive(true);
            equipmentPage.gameObject.SetActive(false);
            statusPage.gameObject.SetActive(true);
            statusPage.gameObject.SetActive(false);
        }

        void Update()
        {
            switch(_gameState)
            {
                case GameState.Init:
                    Init();
                    break;
                case GameState.Play:
                    Playing();
                    break;
                case GameState.Loading:
                    Loading();
                    break;
            }
        }


        void Init()
        {
            
        }
        void Loading()
        {

        }
        void Playing()
        {
            spawnTime += Time.deltaTime;
            if(spawnTime >= 8f)
            {
                spawnTime = 0.0f;
                EventManager.Emit("Respawn",null);
            }
        }
    }
}