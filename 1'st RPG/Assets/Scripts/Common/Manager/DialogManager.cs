using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using common;
public class DialogManager : Singleton<DialogManager>
{
    [SerializeField]
    DialogPage _dialogPage;

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
            
            if(_actionObject != null)
            {
                ObjData data = _actionObject.GetComponent<ObjData>();

                if (_actionObject.layer == LayerMask.NameToLayer("Object"))
                {

                }
                else if(_actionObject.layer == LayerMask.NameToLayer("Npc"))
                {
                    //찾아가는 퀘스트이면 여기서 처리해야함
                    Action(data.id);
                }
                
            }
        }
        
    }
    void Action(int id)
    {
        //QuestManager.Instance.questList.
        int questIdx = 0;
        Dictionary<int, List<QuestData>> database = DataManager.Instance.questList;
        if (database.ContainsKey(id))
        {
            questIdx = database[id].Count != 0 ? database[id][0].questIdx : 0;
            
        }

        if (questIdx != 0)
            CommunicateForQuest(id, database[id][0]);
        else
            Communicate(id);
        _dialogPage.gameObject.SetActive(isAction);
    }
    void CommunicateForQuest(int id, QuestData data)
    {
        int idx;
        Dictionary<int, Quest> dict = PlayerManager.Instance._questManager.currentQuests[data._type - '0'];

        if (dict.ContainsKey(data.questIdx + id))
        {
            idx = dict[data.questIdx + id].processRate;
        }
        else
            idx = data.questIdx;

        string textData = GetData(id+idx, dialogIdx);
        
        if (textData == null)
        {
            idx %= 3;
            switch(idx)
            {
                case 0:  //퀘스트 완료
                    DataManager.Instance.questList[id].Remove(data);
                    dict.Remove(data.questIdx + id);
                    PlayerManager.Instance._playerStat.AddExp(data.exp);
                    break;
                case 1: //퀘스트 시작
                    dict.Add(data.questIdx + id, new Quest(data));
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
    

    
}
