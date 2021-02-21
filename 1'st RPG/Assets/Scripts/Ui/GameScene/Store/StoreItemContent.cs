using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class StoreItemContent : MonoBehaviour,IPointerClickHandler
{
    [SerializeField]
    Image _itemIcon;
    [SerializeField]
    Text _itemPrice;
    [SerializeField]
    Text _itemName;
    public ItemData data;

    static public StoreItemContent selectedStoreItem;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void UpdateInform()
    {
        _itemIcon.sprite = data.Sprite;
        _itemPrice.text = data.Price.ToString();
        _itemName.text = data.Name;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (selectedStoreItem != null)
            selectedStoreItem.GetComponent<Image>().color = new Color(0.9f, 0.3f, 0.3f);
        selectedStoreItem = this;
        selectedStoreItem.GetComponent<Image>().color = new Color(0.5f, 0.3f, 0.3f);
    }
}
