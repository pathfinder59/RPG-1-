using common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    List<Transform> _itemSlotList;
    [SerializeField]
    GameObject ItemUiPrefab;
    private void Awake()
    {
    }
    private void OnEnable()
    {
        EventManager.Emit("UpdateInventory");
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool AddItem(ItemData data)
    {
        var itemSlot = _itemSlotList.FirstOrDefault(_ => _.childCount == 0)?? null;
        if (itemSlot == null)
            return false;

        var go = Instantiate(ItemUiPrefab);
        go.GetComponent<ItemUi>().Data = data;
        go.GetComponent<ItemUi>().UpdateData();
        go.transform.SetParent(itemSlot);
        
        return true;
    }
}
