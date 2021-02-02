using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using common;
public class StorePage : MonoBehaviour
{
    [SerializeField]
    GameObject _prefab;
    [SerializeField]
    List<ItemData> _datas;
    [SerializeField]
    InventoryPage inventoryPage;
    [SerializeField]
    Transform _content;
    ItemData selectedData;
    private void OnEnable()
    {
        selectedData = null;
    }
    void Start()
    {
        foreach(ItemData data in _datas )
        {
            var go = Instantiate(_prefab);
            go.transform.SetParent(_content);
            go.GetComponent<StoreItemContent>().data = data;
            go.GetComponent<StoreItemContent>().UpdateInform();
        }
    }

    void Update()
    {
        
    }

    public void SelectItem(ItemData data)
    {
        selectedData = data;
    }
    public void BuyItem()
    {
        if (selectedData == null)
            return;
        if(selectedData.Price <= PlayerManager.Instance.Money)
        {
            PlayerManager.Instance.Money -= selectedData.Price;
            EventManager.Emit("UpdateMoney");
            if(selectedData.Category >= 100)
            {
                //물약 처리
                PlayerManager.Instance.NumHp++;
                EventManager.Emit("UpdatePotion");
            }
            else
            {
                inventoryPage.AddItem(selectedData);
                    
            }
        }
    }
}
