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
        
        if(Input.GetAxis("Jump") > 0)
        {
            
            if(_actionObject != null)
            {
                ObjData data = _actionObject.GetComponent<ObjData>();

                if (_actionObject.layer == LayerMask.NameToLayer("Object"))
                {

                }
                else if(_actionObject.layer == LayerMask.NameToLayer("Npc"))
                {
                    Action(data.id);
                }
                
            }
        }
        
    }
    void Action(int id)
    {
        Communicate(id);
        _dialogPage.gameObject.SetActive(isAction);
    }
    void Communicate(int id)
    {
        string textData = GetData(id, dialogIdx);
        if (textData == null)
        {
            isAction = false;
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
