using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene.Skill;

namespace common
{
    public class GameManager : Singleton<GameManager>
    {

        string playerClass;
        enum GameState
        {
            Start,Loading,Play
        }

        GameState _gameState;

     
        void Start()
        {
            playerClass = "Warrior"; // 완성시에는 이 값은 캐릭터 선택화면에서 결정할것
        }

        void Update()
        {

        }
    }
}