using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using common;
public class DialogManager : Singleton<DialogManager>
{
    [SerializeField]
    DialogPage _dialogPage;

    GameObject _actionObject;

    //int dialogIdx;
    public GameObject ActionObject { get { return _actionObject; } set { _actionObject = value; } }

    IDictionary<int, List<string>> dialogDatas;
    void Start()
    {
        //dialogIdx = 0;
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
        /*
        if(Input.GetKeyDown("Jump"))
        {
            if(_actionObject != null)
            {
                if(_actionObject.layer == LayerMask.NameToLayer("Object"))
                {

                }
                else if(_actionObject.layer == LayerMask.NameToLayer("Npc"))
                {
                    //
                    //string txt = getData()    
                }
                
            }
        }
        */
    }

    public string getData(int key,int idx)
    {
        if (!dialogDatas.ContainsKey(key))
            return null;
        else
            return dialogDatas[key][idx];
    }

}
