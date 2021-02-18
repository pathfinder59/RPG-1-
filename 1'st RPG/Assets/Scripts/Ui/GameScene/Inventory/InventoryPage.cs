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
    InventoryBtn initialBtn;
    ICommander curCommander;

    public Text ItemDescriptor;

    private void Awake()
    {
        curCommander = initialBtn;
        curCommander.On();
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
        if(curCommander != null)
            curCommander.Off();
        SetCommander(commander);
        commander.On();
    }
    public void SetCommander(ICommander commander)
    {
        curCommander = commander;
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
