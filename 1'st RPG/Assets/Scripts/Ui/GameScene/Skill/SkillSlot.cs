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
        _data = null;
    }

    void Update()
    {
        if (_data != null)
        {
            if (coolDown == 0)
                return;
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

        if (_coverImage.activeInHierarchy)
            SetSkill();
        else
        {
            if (GameSceneManager.Instance.IsAct)
                return;
            UseSkill();
        }
    }
    void SetSkill()
    {
        if (coolDown != 0)
            return;
        SkillSlot[] slotList = _parent.GetComponentsInChildren<SkillSlot>();
        float time = 0.0f;

        foreach (var slot in slotList)
        {
            if (slot._data == Skill_Icon.ClickedData)
            {
                slot._data = null;
                slot.GetComponent<Button>().image.sprite = null;
                time = slot.coolDown;
                slot.coolDown = 0;
                slot._text.text = "";
                _Background.sprite = null;
            }
        }
        _coverImage.SetActive(false);
        _data = Skill_Icon.ClickedData;
        coolDown = time;
        GetComponent<Button>().image.sprite = _data.Sprite;
        _Background.sprite = _data.Sprite;
        Skill_Icon.ClickedData = null;
    }
    void UseSkill()
    {
        if (_data == null || coolDown != 0)
            return;
        if (!_playerFsm.isUsingSkill)
        {
            StartCoroutine(_playerFsm.UseSkill(_data));
            coolDown = _data.CoolDown;
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
