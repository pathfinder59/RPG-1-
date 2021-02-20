using common;
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
                go.GetComponent<Item>().Data = data;
                go.GetComponent<Item>().UpdateData();
                go.transform.SetParent(transform.GetChild(i));
                return true;
            }
        }
        return false;
    }

    public override void PointerClickItem(Item item)
    {
        int category = item.GetCategory();

        if (GameSceneManager.Instance.EquipmentPage.EquipSlots[category].transform.childCount == 0)
            item.transform.SetParent(GameSceneManager.Instance.EquipmentPage.EquipSlots[category].transform);
        else
        {
            var obj = GameSceneManager.Instance.EquipmentPage.EquipSlots[category].transform.GetChild(0);
            obj.SetParent(item.transform.parent);
            item.transform.SetParent(GameSceneManager.Instance.EquipmentPage.EquipSlots[category].transform);
        }
        EventManager.Emit("UpdatePlayerEquip");
    }
}
