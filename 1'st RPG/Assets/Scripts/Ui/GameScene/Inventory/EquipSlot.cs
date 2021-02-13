﻿using common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipSlot : MonoBehaviour, IDropHandler
{
    public int itemCategory; // 아이템 종류
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        SetEquip();
    }

    void SetEquip()
    {
        if (itemCategory != ItemUi.DragedObject.GetComponent<ItemUi>().Data.Category)
            return;
        if (transform.childCount != 0)
        {
            transform.GetChild(0).SetParent(ItemUi.PrevSlot);
        }

        ItemUi.DragedObject.transform.SetParent(transform);
        EventManager.Emit("UpdatePlayerEquip");
    }
}
