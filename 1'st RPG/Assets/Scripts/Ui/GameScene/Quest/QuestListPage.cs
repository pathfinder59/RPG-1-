﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using common;
public class QuestListPage : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Transform _questContent;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddContent(QuestData data)
    {
        var content = ObjectPoolManager.Instance.Spawn("QuestContent").GetComponent<QuestContent>();
        content.data = data;
        content.transform.SetParent(_questContent);
    }
    public void ClosePage(GameObject obj = null)
    {
        gameObject.SetActive(false);
        
    }
}
