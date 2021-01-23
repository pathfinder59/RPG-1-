using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using common;
using GameScene.Skill;
public class Skill_Icon : MonoBehaviour
{
    [SerializeField]
    GameObject _coverImage;
    [SerializeField]
    SkillData data;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void OnClickIcon()
    {
        if (!_coverImage.activeInHierarchy)
        {
            _coverImage.SetActive(true);
            GameManager.Instance.skillData = data;
        }
    }
}
