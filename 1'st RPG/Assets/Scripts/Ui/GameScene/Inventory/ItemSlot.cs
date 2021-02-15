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
        ItemData data = ItemUi.DragedObject.GetComponent<ItemUi>().Data;
        Transform prevSlot = ItemUi.PrevSlot;


        if (transform.childCount != 0)
        {
            if (prevSlot.GetComponent<EquipSlot>() != null)
            {
                if (transform.GetChild(0).GetComponent<ItemUi>().Data.Category != data.Category)
                    return;
            }
            transform.GetChild(0).SetParent(ItemUi.PrevSlot);
        }

        ItemUi.DragedObject.transform.SetParent(transform);
        EventManager.Emit("UpdatePlayerEquip");
    }
}
