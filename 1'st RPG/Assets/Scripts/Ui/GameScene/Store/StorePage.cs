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
    Transform _content;

    private void OnEnable()
    {
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

    public void BuyItem()
    {
        ItemData selectedData = StoreItemContent.selectedStoreItem.data;
        if (selectedData == null)
            return;
        if(selectedData.Price <= PlayerManager.Instance.Money)
        {
            if(selectedData.Category >= 100)
            {
                //물약 처리
                PlayerManager.Instance.NumHp++;
                EventManager.Emit("UpdatePotion");
            }
            else
            {
                if (!GameSceneManager.Instance.Inventory.AddItem(selectedData))
                    return;
                    
            }
            PlayerManager.Instance.Money -= selectedData.Price;
            EventManager.Emit("UpdateMoney");
        }
    }

    public void ClickExitBtn()
    {
        if (StoreItemContent.selectedStoreItem != null)
        {
            StoreItemContent.selectedStoreItem.GetComponent<Image>().color = new Color(0.9f, 0.3f, 0.3f);
            StoreItemContent.selectedStoreItem = null;
        }
    }
}
