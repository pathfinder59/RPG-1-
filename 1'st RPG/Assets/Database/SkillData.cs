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
        Sprite _sprite;
        [SerializeField]
        int _range;
        [SerializeField]
        float _coolDown;
        [SerializeField]
        string _content;
        [SerializeField]
        string _trigger = null;
        [SerializeField]
        float _time;
        [SerializeField]
        bool isTargeting;
        [SerializeField]
        float distance;

        public string Name => _name;
        public Sprite Sprite => _sprite;
        public int Range => _range;
        public float CoolDown => _coolDown;
        public string Content => _content;
        public string Trigger => _trigger;
        public float Time => _time;
        public bool IsTargeting => isTargeting;

        public float Distance => distance;

    }
}