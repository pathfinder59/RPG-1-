using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameScene.Skill;

namespace common
{
    public class GameManager : Singleton<GameManager>
    {

        enum GameState
        {
            Start,Loading,Play
        }

        GameState _gameState;

     
        void Start()
        {
            
        }

        void Update()
        {

        }
    }
}