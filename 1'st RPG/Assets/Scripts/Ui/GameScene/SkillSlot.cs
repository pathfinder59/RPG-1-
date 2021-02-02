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
    [SerializeField]
    Image _Background;
    [SerializeField]
    Text _text;

    PlayableFSM _playerFsm;

    float coolDown;

    void Start()
    {
        _playerFsm = GameObject.Find("Player").GetComponent<PlayableFSM>();
        coolDown = 0;
    }

    void Update()
    {
        if (_data != null)
        {
            coolDown = Mathf.Clamp(coolDown - Time.deltaTime, 0, _data.CoolDown);
            GetComponent<Button>().image.fillAmount = 1 - (coolDown / _data.CoolDown);
            if(coolDown != 0)
                _text.text = ((int)coolDown).ToString();
            else
                _text.text = "";
        }
    }

    public void OnClickBtn()
    {
        if(_coverImage.activeInHierarchy)
        {
            if (coolDown != 0)
                return;
            SkillSlot[] slotList = _parent.GetComponentsInChildren<SkillSlot>();
            foreach(var slot in slotList)
            {
                if (slot._data == PlayerManager.Instance.skillData)
                {
                    slot._data = null;
                    slot.gameObject.GetComponent<Button>().image.sprite = null;
                    _Background.sprite = null;
                }
            }


            _coverImage.SetActive(false);
            _data = PlayerManager.Instance.skillData;
            GetComponent<Button>().image.sprite = _data.Sprite;
            _Background.sprite = _data.Sprite;

        }
        else
        {
            if (!_playerFsm.isUsingSkill)
            {
                if (coolDown != 0)
                    return;
                if (_data.IsTargeting)
                {
                    if (_playerFsm.Target == null)
                        return;
                    StartCoroutine("InDistance");
                    
                    //타겟팅일경우 상대와의 거리가 사정거리 안까지 올때 이동 후 코루틴 실행 ,   아직 미구현
                }

                //쿨타임 적용부분
                StartCoroutine(_playerFsm.UseSkill(_data));

                coolDown = _data.CoolDown;
            }
        }

    }
    IEnumerator InDistance()
    {
        while(true)
        {

            if(_playerFsm.Target == null || Vector3.Distance(_playerFsm.Target.transform.position,gameObject.transform.position) <= _data.Distance)
            {
                yield break;
            }
            yield return null;
        }
    }

}
