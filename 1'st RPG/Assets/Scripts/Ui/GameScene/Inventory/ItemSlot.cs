using common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void OnDrop(PointerEventData eventData)
    {
        SetItem();
    }

    void SetItem()
    {
        ItemData data = Item.DragedObject.GetComponent<Item>().Data;
        Transform prevSlot = Item.PrevSlot;


        if (transform.childCount != 0)
        {
            if (prevSlot.GetComponent<EquipSlot>() != null)
            {
                if (transform.GetChild(0).GetComponent<Item>().Data.Category != data.Category)
                    return;
            }
            transform.GetChild(0).SetParent(Item.PrevSlot);
        }

        Item.DragedObject.transform.SetParent(transform);
        EventManager.Emit("UpdatePlayerEquip");
    }
}
