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

    void Start()
    {
        EventManager.On("QuestContentClick", UpdateDescriptor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateDescriptor(GameObject obj = null)
    {
        QuestData data = QuestContent.clickedContent.data;
        Quest quest = PlayerManager.Instance.gameObject.GetComponent<QuestManager>().currentQuests[data._type][data.client + data.questIdx] ?? null;

        string questType = data._type == 0 ? "[토벌] " : (data._type == 1 ? "[말걸기] " : "[수집] ");
        descriptor.text = questType + data.descript;

        if (data._type != 1 && quest != null)
            descriptor.text += string.Format("\n{0}/{1}", quest.num, data.num);
    }
}
