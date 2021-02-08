using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using common;
public class QuestListPage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Transform _questContents;
    void Start()
    {
        EventManager.On("CloseQuestPage", SetActivePage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddContent(QuestData data)
    {
        var content = ObjectPoolManager.Instance.Spawn("QuestContent").GetComponent<QuestContent>();
        content.data = data;
        content.transform.SetParent(_questContents);
    }
    public void SetActivePage(GameObject obj = null)
    {
        gameObject.SetActive(false);
    }
}
