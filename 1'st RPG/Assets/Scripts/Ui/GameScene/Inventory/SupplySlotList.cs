using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplySlotList : ItemSlotList
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
                go.GetComponent<ItemUi>().numItem = 1;
                go.GetComponent<ItemUi>().UpdateData();
                go.transform.SetParent(transform.GetChild(i));
                return true;
            }
            else
            {
                ItemUi item = transform.GetChild(i).GetChild(0).GetComponent<ItemUi>();
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
                ItemUi item = transform.GetChild(i).GetChild(0).GetComponent<ItemUi>();
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

}
