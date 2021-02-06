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

        int questIdx = 0;
        QuestData questData;

        //완료된 말걸기 퀘스트가 존재하지않는 경우 호출되어야함.
        //말걸기 퀘스트 같은경우에는 키값이 클라이언트 - 타겟인 데이터베이스 두곳에 존재함.
        //완료 시에는 두 데이터베이스에서 삭제하도록한다. 
        //여기서 퀘스트 처리를 할때마다 모든 npc들이 자기 키값에 해당하는 리스트를 확인해서 있을경우 머리위에 아이콘이 생성되도록 하자!.
        //우선 순위는 완료> 시작> 진행중
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
        Dictionary<int, Quest> currentQuests = PlayerManager.Instance._questManager.currentQuests[data._type - '0'];

        if (currentQuests.ContainsKey(data.questIdx + data.client))
        {
            idx = currentQuests[data.questIdx + data.client].processRate;
            idx = data.questIdx - 1 + idx;
        }
        else
            idx = data.questIdx;

        string textData = GetData(data.client + idx, dialogIdx);
        
        if (textData == null)
        {
            idx %= 3;
            switch(idx)
            {
                case 0:  //퀘스트 완료
                    DataManager.Instance.questList[data.client].Remove(data);
                    currentQuests.Remove(data.questIdx + data.client);
                    PlayerManager.Instance._playerStat.AddExp(data.exp);
                    break;
                case 1: //퀘스트 시작
                    //말걸기 퀘스트일 경우에는 완료타겟의 아이디를 키값으로 datamager의 퀘스트리스트에도 완료형 퀘스트를 등록하도록 하자.
                    if (data._type == '1')
                    {
                        //DataManager.Instance.questList[data.target].Add(new QuestData(data.questIdx,data.client,data.target,data._type,data.num,data.exp));
                        currentQuests.Add(data.questIdx + data.client, new Quest(data,true));
                    }
                    else
                        currentQuests.Add(data.questIdx + data.client, new Quest(data));
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
