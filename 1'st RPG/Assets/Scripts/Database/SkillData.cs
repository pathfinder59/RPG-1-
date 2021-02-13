using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene.Skill
{
    [CreateAssetMenu(fileName = "Skill Data", menuName ="Scriptable Object/SkillData")]
    public class SkillData : ScriptableObject
    {
        
        [SerializeField]
        string _name;
        [SerializeField]
        int _id;

        [SerializeField]
        Sprite _sprite;
        [SerializeField]
        float _coolDown;
        [SerializeField]
        float _time;

        [SerializeField]
        float distance;

        public string Name => _name;
        public Sprite Sprite => _sprite;
        public float CoolDown => _coolDown;
        public float Time => _time;
        public float Distance => distance;

    }
}