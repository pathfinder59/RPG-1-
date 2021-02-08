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

    void Awake()
    {
        EventManager.On("QuestContentClick", UpdateDescriptor);
        EventManager.On("CloseQuestPage", ResetDescriptor);
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateDescriptor(GameObject obj = null)
    {
        QuestData data = QuestContent.clickedContent.data;
        Quest quest = null;
        if (PlayerManager.Instance._questManager.currentQuests[data._type - '0'].ContainsKey(data.client + data.questIdx))
            quest = PlayerManager.Instance._questManager.currentQuests[data._type - '0'][data.client + data.questIdx];
        
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
