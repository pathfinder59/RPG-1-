using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using common;
using GameScene.Skill;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    [SerializeField]
    SkillData _data;
    [SerializeField]
    GameObject _coverImage;
    [SerializeField]
    GameObject _parent;

    PlayerFSM _playerFsm;
    void Start()
    {
        _playerFsm = GameObject.Find("Player").GetComponent<PlayerFSM>();
    }

    void Update()
    {
        
    }

    public void OnClickBtn()
    {
        if(_coverImage.activeInHierarchy)
        {
            SkillSlot[] slotList = _parent.GetComponentsInChildren<SkillSlot>();
            foreach(var slot in slotList)
            {
                if (slot._data == PlayerManager.Instance.skillData)
                {
                    slot._data = null;
                    slot.gameObject.GetComponent<Button>().image.sprite = null;
                }
            }


            _coverImage.SetActive(false);
            _data = PlayerManager.Instance.skillData;
            GetComponent<Button>().image.sprite = _data._sprite;


        }
        else
        {
            if (!_playerFsm.isUsingSkill)
            {
                StartCoroutine(_playerFsm.UseSkill(_data));
            }
        }

    }
}
