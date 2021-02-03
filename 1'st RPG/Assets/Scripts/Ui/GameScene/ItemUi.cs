using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using common;
public class ItemUi : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    CanvasGroup canvasGroup;

    public static GameObject DragedObject;
    public static Transform ItemSlot;
    public ItemData Data { get; set; }
    [SerializeField]
    Image _image;

    int numItem;

    Transform InventoryPage;
    
    void Start()
    {
        InventoryPage = GameManager.Instance.Inventory.transform;
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void OnEnable()
    {
        numItem = 0;
    }
    void Update()
    {

    }


    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragedObject = transform.gameObject;
        ItemSlot = transform.parent;
        transform.SetParent(InventoryPage);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragedObject = null;
        if(InventoryPage == transform.parent)
            transform.SetParent(ItemSlot);
        canvasGroup.blocksRaycasts = true;
    }

    public void UpdateData()
    {
        _image.sprite = Data.Sprite;
    }

    public void OnClick()
    {
        /*
        if (Data == null) return;

        EquipBtn btn = equipmentPage.EquipUis[Data.Category];
        if (btn.Data == null)
        {
            btn.Data = Data;
            Data = null;
        }
        else
        {
            ItemData temp = btn.Data;
            btn.Data = Data;
            Data = temp;
        }
        EventManager.Emit("UpdateInventory", null);
        EventManager.Emit("UpdatePlayerEquip", null);
        EventManager.Emit("UpdateStatus");
        */
    }
}
