using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using GameScene.Skill;
using System.Linq;
using common;
using UnityEngine.UI;

public class SkillScrollViewContent : MonoBehaviour
{
    [SerializeField]
    GameObject _SkillPage;
    [SerializeField]
    GameObject _SettingPage;

    [SerializeField]
    GameObject _panelPrefab;
    [SerializeField]
    Text _skillPointText;

    void Start()
    {
        GetCurrentSkills();
    }

    void Update()
    {
        
    }
    public void GetCurrentSkills()
    {
        List<SkillDatabase> databaseList = DataManager.Instance.skillDatabases;
        foreach (var skillData in databaseList[0].SkillList)
        {
            var go = Instantiate(_panelPrefab);
            go.transform.SetParent(transform);
            go.GetComponent<Skill_Icon>().data = skillData;
        }

        SkillDatabase database = databaseList.FirstOrDefault(c => c.ClassName == PlayerManager.Instance._playerStat.ClassType) ?? null;
        if (database)
        {
            foreach (var skillData in database.SkillList)
            {
                var go = Instantiate(_panelPrefab);
                go.transform.SetParent(transform);
                go.GetComponent<Skill_Icon>().data = skillData;
            }
        }
    }
    private void LateUpdate()
    {
        _skillPointText.text = "Skill Point: " + PlayerManager.Instance._playerStat.SkillPoint.ToString();
    }

    public void ClickCloseBtn()
    {
        if(Skill_Icon.ClickedData == null)
        {
            _SettingPage.SetActive(true);
            _SkillPage.SetActive(false);
        }
    }
}
