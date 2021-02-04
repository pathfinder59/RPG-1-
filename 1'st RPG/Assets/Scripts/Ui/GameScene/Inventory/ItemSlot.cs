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
        EquipSlot slot = ItemUi.DragedObject.transform.parent.GetComponent<EquipSlot>();
        if (slot != null)
        {
            if (slot.itemCategory != ItemUi.DragedObject.transform.GetComponent<ItemUi>().Data.Category)
                return;
        }

        if (transform.childCount != 0)
        {
            transform.GetChild(0).SetParent(ItemUi.ItemSlot);
        }

        ItemUi.DragedObject.transform.SetParent(transform);
        EventManager.Emit("UpdatePlayerEquip");
    }
}
