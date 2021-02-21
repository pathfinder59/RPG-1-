using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using common;
public class DialogManager : Singleton<DialogManager>
{
    [SerializeField]
    DialogPage _dialogPage;
    [SerializeField]
    QuestListPage questListPage;

    int dialogIdx;

    IDictionary<int, List<string>> dlgDatas;
    IDictionary<int, List<DialogData>> questDlgDatas;
    void Start()
    {
        dialogIdx = 0;

        dlgDatas = new Dictionary<int, List<string>>();
        questDlgDatas = new Dictionary<int, List<DialogData>>();
        foreach (DialogData data in DataManager.Instance.dialogDatabase)  //npc별 퀘스트 컬렉션 
        {
            if (!dlgDatas.ContainsKey(data._id))
                dlgDatas.Add(data._id, new List<string>());
            dlgDatas[data._id] = data._dialogs;


        }

        foreach (QuestData data in DataManager.Instance.questDatabase) //퀘스트용 대화 컬렉션
        {
            if (!questDlgDatas.ContainsKey(data.questIdx))
            {
                questDlgDatas.Add(data.questIdx, new List<DialogData>());
            }
            for (int i = 0; i < 3; ++i)
                questDlgDatas[data.questIdx] = data._dialogs.GetRange(0, data._dialogs.Count);
        }
    }

    void Update()
    {        
        if(Input.GetButtonDown("Jump") && !GameSceneManager.Instance._interactBtn.activeInHierarchy)
        {
            if (questListPage.gameObject.activeInHierarchy)
            {
                return;
            }
            if(GameSceneManager.Instance.ActionObject != null)
            {
                Interact(GameSceneManager.Instance.ActionObject);
            }
        }
    }
    public void Interact(GameObject ActionObj)
    {
        ObjData objData = ActionObj.GetComponent<ObjData>();

        if (ActionObj.layer == LayerMask.NameToLayer("Object"))
        {
            //오브젝트 상호작용용 ex)채집
        }
        else if (ActionObj.layer == LayerMask.NameToLayer("Npc"))
        {
            Action(objData.id);
        }
    }
    public void Action(int id)
    {
        Dictionary<int, List<QuestData>> database = DataManager.Instance.questDict;
        QuestData questData = null;

        if (database.ContainsKey(id))
            questData = database[id].Find(_ => _.isActive == true);

        if (questData != null)
            CommunicateForQuest(id);
        else
            Communicate(id);
    }

    void CommunicateForQuest(int id)
    {
        if (QuestManager.Instance.OpenQuestList(id))
            return;

        QuestData clickedQuestData = QuestContent.clickedContent.data;
        Dictionary<int, Quest> currentQuests = QuestManager.Instance.currentQuests[clickedQuestData._type - '0'];

        int processRate = QuestManager.Instance.GetQuestProceesRate(id, currentQuests, clickedQuestData);
        string textData = GetData( questDlgDatas[clickedQuestData.questIdx][processRate-1]._dialogs, dialogIdx);
        
        if (textData == null)
        {
            QuestManager.Instance.ProcessQuest(id, processRate, clickedQuestData, currentQuests);
            dialogIdx = 0;
            return;
        }
        _dialogPage._text.text = textData;
        UiController.Instance.OpenDialogPage(true);
        dialogIdx++;

    }
    void Communicate(int id)
    {
        string textData = GetData(dlgDatas[id], dialogIdx);
        if (textData == null)
        {
            UiController.Instance.OpenDialogPage(false);
            dialogIdx = 0;
            return;
        }
        _dialogPage._text.text = textData;
        UiController.Instance.OpenDialogPage(true);
        dialogIdx++;
    }

    string GetData(List<string> strings,int idx)
    {
        if (idx == strings.Count)
            return null;
        else
            return strings[idx];
    }
    
    
}
