using common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    ItemSlotList _equipList;
    [SerializeField]
    ItemSlotList _supplyList;

    [SerializeField]
    GameObject ItemUiPrefab;

    [SerializeField]
    InventoryBtn initialBtn; //장비 or 소비 선택버튼
    ICommander curCategory;

    public Text ItemDescriptor;

    private void Awake()
    {
        curCategory = initialBtn;
    }
    private void OnEnable()
    {
        curCategory.On();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ClickPageBtn(ICommander commander)
    {
        if(curCategory != null)
            curCategory.Off();
        SetCommander(commander);
        commander.On();
    }
    public void SetCommander(ICommander commander)
    {
        curCategory = commander;
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
