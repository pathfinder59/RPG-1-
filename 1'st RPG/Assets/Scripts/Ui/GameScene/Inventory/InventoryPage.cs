using common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    ItemSlotList _equipList;
    [SerializeField]
    ItemSlotList _supplyList;

    [SerializeField]
    GameObject ItemUiPrefab;

    [SerializeField]
    InventoryBtn initialBtn;
    ICommander curPageBtn;

    private void Awake()
    {
        curPageBtn = initialBtn;
        curPageBtn.On();
    }
    private void OnEnable()
    {
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ClickPageBtn(ICommander commander)
    {
        if(curPageBtn != null)
            curPageBtn.Off();
        SetCommander(commander);
        commander.On();
    }
    public void SetCommander(ICommander commander)
    {
        curPageBtn = commander;
    }
    public bool AddItem(ItemData data)
    {
        ItemSlotList slotList;

        if (data.Category < 4)
            slotList = _equipList;
        else
            slotList = _supplyList;

        if (slotList.AddItem(data, ItemUiPrefab))
            return true;
        else
            return false;
    }
}
