using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using GameScene.Skill;
using System.Linq;
using common;
using UnityEngine.UI;

public class SkillScrollViewContent : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    GameObject _panelPrefab;
    [SerializeField]
    Text _skillPointText;
    [SerializeField]
    List<SkillDatabase> databaseList;
    void Start()
    {
        foreach(var skillData in databaseList[0].SkillList)
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

    // Update is called once per frame
    void Update()
    {
        _skillPointText.text = "Skill Point: " + PlayerManager.Instance._playerStat.SkillPoint.ToString();
    }
}
