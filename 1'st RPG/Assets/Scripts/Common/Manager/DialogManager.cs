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

    GameObject _actionObject;

    int dialogIdx;
    public bool isAction;
    public GameObject ActionObject { get { return _actionObject; } set { _actionObject = value; } }

    IDictionary<int, List<string>> dialogDatas;
    void Start()
    {
        isAction = false;
        dialogIdx = 0;
        _actionObject = null;
        dialogDatas = new Dictionary<int, List<string>>();

        foreach(DialogData data in DataManager.Instance.dialogDatabase)
        {
            if (!dialogDatas.ContainsKey(data._id))
                dialogDatas.Add(data._id, new List<string>());
            dialogDatas[data._id] = data._dialogs;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetButtonDown("Jump") )
        {
            if (questListPage.gameObject.activeInHierarchy)
            {
                return;
            }
            if(_actionObject != null)
            {
                Interact();
            }
        }
    }
    public void Interact()
    {
        ObjData objData = _actionObject.GetComponent<ObjData>();

        if (_actionObject.layer == LayerMask.NameToLayer("Object"))
        {
            //오브젝트 상호작용용 ex)채집
        }
        else if (_actionObject.layer == LayerMask.NameToLayer("Npc"))
        {
            Action(objData.id);
        }
    }
    void Action(int id)
    {
        Dictionary<int, List<QuestData>> database = DataManager.Instance.questList;
        QuestData questData = null;

        if (database.ContainsKey(id))
            questData = database[id].Find(_ => _.isActive == true);

        if (questData != null)
            CommunicateForQuest(id);
        else
            Communicate(id);

        _dialogPage.gameObject.SetActive(isAction);
    }
    public bool OpenQuestList(int id)
    {
        if (QuestContent.clickedContent != null)
            return false;
        else
        {
            questListPage.gameObject.SetActive(true);
            Dictionary<int, List<QuestData>> database = DataManager.Instance.questList;
            foreach(QuestData data in database[id])
            {
                if (data.isActive)
                    questListPage.AddContent(data);
            }
            EventManager.Emit("UpdataQuestPage");
            return true;
        }
    }
    void CommunicateForQuest(int id)
    {
        int processRate;
        if (OpenQuestList(id))
            return;
        QuestData questData = QuestContent.clickedContent.data;
        Dictionary<int, Quest> currentQuests = PlayerManager.Instance._questManager.currentQuests[questData._type - '0'];

        getQuestProceesRate(out processRate, id, currentQuests, questData);

        string textData = GetData(questData.client + processRate, dialogIdx);
        
        if (textData == null)
        {
            QuestContent.clickedContent = null;
            processRate %= 3;
            switch(processRate)
            {
                case 0:  //퀘스트 완료
                    PlayerManager.Instance._playerStat.gameObject.GetComponent<QuestManager>().ClearQuest(currentQuests, questData, id);
                    break;
                case 1: //퀘스트 시작
                    PlayerManager.Instance._playerStat.gameObject.GetComponent<QuestManager>().StartQuest(currentQuests, questData, id);
                    break;
                case 2: //퀘스트 진행중
                    break;
            }

            isAction = false;
            dialogIdx = 0;
            return;
        }
        _dialogPage._text.text = textData;
        isAction = true;
        dialogIdx++;
        //여기서 갈랫길 진행도에 따라서 출력할 텍스트 인덱스 결정

    }
    void Communicate(int id)
    {
        string textData = GetData(id, dialogIdx);
        if (textData == null)
        {
            isAction = false;
            dialogIdx = 0;
            return;
        }
        _dialogPage._text.text = textData;
        isAction = true;
        dialogIdx++;
    }

    string GetData(int key,int idx)
    {
        if (idx == dialogDatas[key].Count)
            return null;
        else
            return dialogDatas[key][idx];
    }
    
    void getQuestProceesRate(out int idx, int id, Dictionary<int, Quest> QuestList, QuestData data)
    {
        if (QuestList.ContainsKey(data.questIdx + data.client))
        {
            idx = QuestList[data.questIdx + data.client].processRate;
            idx = data.questIdx - 1 + idx;
            if (data._type == '1' && data.target != id)
            {
                idx = data.questIdx - 1 + 2; //진행중으로 이동
            }
        }
        else
            idx = data.questIdx;
    }
}
