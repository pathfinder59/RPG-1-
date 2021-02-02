using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemContent : MonoBehaviour
{
    [SerializeField]
    Image _itemIcon;
    [SerializeField]
    Text _itemPrice;

    public ItemData data;

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
    }

    public void OnClick()
    {
        transform.GetComponentInParent<StorePage>().SelectItem(data);
    }
}
