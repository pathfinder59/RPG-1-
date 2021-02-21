using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBtn : MonoBehaviour,ICommand
{
    [SerializeField]
    InventoryPage _inventoryPage;
    [SerializeField]
    ItemSlotList _inventoryList;
    public void Off()
    {
        _inventoryList.gameObject.SetActive(false);
    }

    public void On()
    {
        _inventoryList.gameObject.SetActive(true);
        GameSceneManager.Instance.curItemSlotList = _inventoryList;
    }

    public void OnClick()
    {
        _inventoryPage.ClickPageBtn(this);
    }
}
