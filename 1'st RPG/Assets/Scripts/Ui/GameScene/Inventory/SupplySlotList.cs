using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplySlotList : ItemSlotList
{
    public override bool AddItem(ItemData data, GameObject itemUi)
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).childCount == 0)
            {
                GameObject go = Instantiate(itemUi);
                go.GetComponent<Item>().Data = data;
                go.GetComponent<Item>().numItem = 1;
                go.GetComponent<Item>().UpdateData();
                go.transform.SetParent(transform.GetChild(i));
                return true;
            }
            else
            {
                Item item = transform.GetChild(i).GetChild(0).GetComponent<Item>();
                if(item.Data == data && item.numItem < 10)
                {
                    item.numItem++;
                    item.UpdateData();
                    return true;
                }
            }
        }
        return false;
    }

    public override void SubItem(int category)
    {
        for (int i = 0; i < transform.childCount; ++i)
        {
            if (transform.GetChild(i).childCount != 0)
            {
                Item item = transform.GetChild(i).GetChild(0).GetComponent<Item>();
                if(item.Data.Category == category)
                {
                    item.numItem--;
                    if (item.numItem == 0)
                    {
                        Destroy(transform.GetChild(i).GetChild(0).gameObject);
                    }
                    else
                    {
                        item.UpdateData();
                    }
                    return;
                }
            }
        }

    }
    public override void PointerClickItem(Item item)
    {
        if (GameSceneManager.Instance.UsePotion())
        {
            item.UpdateData();
        }
    }
}
