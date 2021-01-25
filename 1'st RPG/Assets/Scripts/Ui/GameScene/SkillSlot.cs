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

    public void OnClickAttackBtn()
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
                if (_data.isTargeting)
                {
                    if (_playerFsm.target = null)
                        return;
                    StartCoroutine("InDistance");
                    if (_playerFsm.target == null)
                        return;
                    //타겟팅일경우 상대와의 거리가 사정거리 안까지 올때 이동 후 코루틴 실행
                }

                //쿨타임 적용부분
                StartCoroutine(_playerFsm.UseSkill(_data));
            }
        }

    }
    IEnumerator InDistance()
    {
        while(true)
        {

            if(_playerFsm.target == null || Vector3.Distance(_playerFsm.target.transform.position,gameObject.transform.position) <= _data.distance)
            {
                yield break;
            }
            yield return null;
        }
    }

}
