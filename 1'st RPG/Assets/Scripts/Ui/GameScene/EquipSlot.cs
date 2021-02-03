using common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipSlot : MonoBehaviour, IDropHandler
{
    // Start is called before the first frame update
    
    public int itemCategory;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (itemCategory != ItemUi.DragedObject.GetComponent<ItemUi>().Data.Category)
            return;
        if (transform.childCount != 0)
        {
            transform.GetChild(0).SetParent(ItemUi.ItemSlot);
        }

        ItemUi.DragedObject.transform.SetParent(transform);

        EventManager.Emit("UpdatePlayerEquip");
    }
}
