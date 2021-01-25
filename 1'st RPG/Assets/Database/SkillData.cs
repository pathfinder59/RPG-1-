using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameScene.Skill
{
    [CreateAssetMenu(fileName = "Skill Data", menuName ="Scriptable Object/SkillData")]
    public class SkillData : ScriptableObject
    {
        
        [SerializeField]
        public string _name;
        [SerializeField]
        public Sprite _sprite;
        [SerializeField]
        public int _range;
        [SerializeField]
        public float _coolDown;
        [SerializeField]
        public string _content;
        [SerializeField]
        public string _trigger = null;
        [SerializeField]
        public float _time;
        [SerializeField]
        public bool isTargeting;
        [SerializeField]
        public float distance;

    }
}