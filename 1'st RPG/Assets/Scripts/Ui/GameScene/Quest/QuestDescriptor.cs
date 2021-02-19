using common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDescriptor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Text descriptor;

    [SerializeField]
    bool isIcon;
    QuestData data = null;
    public QuestData Data => data;
    void Awake()
    {

        if (!isIcon)
        {
            EventManager.On("QuestContentClick", UpdateData);
            EventManager.On("CloseQuestPage", ResetDescriptor);
        }
        else
            EventManager.On("UpdateDescriptor", UpdateData);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateData(GameObject obj = null)
    {
        if (!isIcon)
        {
            if(QuestContent.clickedContent != null)
                SetData(QuestContent.clickedContent.data);
        }
        UpdateDescriptor();
    }
    public void SetData(QuestData questData)
    {
        data = questData;
    }
    public void UpdateDescriptor()
    {
        if (data == null)
            return;
        Quest quest = null;

        if (QuestManager.Instance.currentQuests[data._type - '0'].ContainsKey(data.questIdx))
            quest = QuestManager.Instance.currentQuests[data._type - '0'][data.questIdx];
        
        string questType = data._type == '0' ? "[토벌] " : (data._type == '1' ? "[말걸기] " : "[수집] ");
        descriptor.text = questType + data.descript;

        if (data._type != '1' && quest != null)
            descriptor.text += string.Format("\n{0}/{1}", data.num - quest.num, data.num); 
    }

    public void ResetDescriptor(GameObject obj = null)
    {
        descriptor.text = "";
    }
}
