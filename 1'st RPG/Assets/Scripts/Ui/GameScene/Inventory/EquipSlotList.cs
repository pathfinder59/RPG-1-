using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlotList : ItemSlotList
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override bool AddItem(ItemData data, GameObject itemUi)
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).childCount == 0)
            {
                GameObject go = Instantiate(itemUi);
                go.GetComponent<ItemUi>().Data = data;
                go.GetComponent<ItemUi>().UpdateData();
                go.transform.SetParent(transform.GetChild(i));
                return true;
            }
        }
        return false;
    }
}
