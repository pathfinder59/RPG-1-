﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using common;
using GameScene.Skill;
public class Skill_Icon : MonoBehaviour
{
    GameObject _coverImage;

    [SerializeField]
    Image _image;
    [SerializeField]
    Text _skillLevel;
    [SerializeField]
    GameObject levelUpBtn;

    public SkillData data;
    void Start()
    {
        _coverImage = GameObject.Find("GameSceneCanvas").transform.GetChild(0).gameObject;
        
        if(data)
        {
            _image.sprite = data.Sprite;
      
        }
    }

    void Update()
    {
        _skillLevel.text = PlayerManager.Instance._playerStatus.SkillLevels[data.Name].ToString();
    }
    public void OnClickIcon()
    {
        if (PlayerManager.Instance._playerStatus.SkillLevels[data.Name] == 0)
            return;
        if (!_coverImage.activeInHierarchy)
        {
            _coverImage.SetActive(true);
            PlayerManager.Instance.skillData = data;
        }
    }
    public void OnClickLevelUpBtn()
    {
        if (PlayerManager.Instance._playerStatus.SkillPoint != 0)
        {
            PlayerManager.Instance._playerStatus.SkillLevels[data.Name]++;
            PlayerManager.Instance._playerStatus.SkillPoint--;

            if (PlayerManager.Instance._playerStatus.SkillLevels[data.Name] >= 3)
                levelUpBtn.SetActive(false);
        }
    }
}
