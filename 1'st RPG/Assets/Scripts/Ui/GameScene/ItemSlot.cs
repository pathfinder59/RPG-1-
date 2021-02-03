using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount != 0)
        {
            transform.GetChild(0).SetParent(ItemUi.ItemSlot);
        }

        ItemUi.DragedObject.transform.SetParent(transform);
    }

    void Start()
    {
        
    }
    void Update()
    {
        
    }
}
