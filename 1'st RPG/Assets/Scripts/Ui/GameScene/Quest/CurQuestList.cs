﻿using common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurQuestList : MonoBehaviour
{
    [SerializeField]
    Transform _viewContent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddDescriptor(QuestData data)
    {
        QuestDescriptor descriptor = ObjectPoolManager.Instance.Spawn("QuestDescriptor").GetComponent<QuestDescriptor>();
        descriptor.SetData(data);
        descriptor.transform.SetParent(_viewContent);
        EventManager.Emit("UpdateDescriptor");
    }
    public void RemoveDescriptor(QuestData data)
    {
        for (int i = 0; i < _viewContent.childCount; ++i)
        {
            if (_viewContent.GetChild(i).GetComponent<QuestDescriptor>().Data == data)
            {
                _viewContent.GetChild(i).gameObject.SetActive(false);
                return;
            }
        }
    }
}
