using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

using common;
public class ItemUi : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    CanvasGroup canvasGroup;

    public static GameObject DragedObject;
    public static GameObject ClickedObject;

    public static Transform PrevSlot;
    public ItemData Data { get; set; }
    [SerializeField]
    Image _image;
    [SerializeField]
    Text _text;

    Transform InventoryPage;

    public int numItem {get;set;}

    void Awake()
    {
    }
    void Start()
    {
        
        InventoryPage = GameSceneManager.Instance.Inventory.transform;
        canvasGroup = GetComponent<CanvasGroup>();
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
        PrevSlot = transform.parent;
        transform.SetParent(InventoryPage);
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragedObject = null;
        if(InventoryPage == transform.parent)
            transform.SetParent(PrevSlot);
        canvasGroup.blocksRaycasts = true;
    }

    public void UpdateData()
    {
        _image.sprite = Data.Sprite;
        if (numItem != 0)
            _text.text = numItem.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickedObject = gameObject;

        if (Data.Category < 3)
        {
            if(GameSceneManager.Instance.EquipmentPage.EquipSlots[Data.Category].transform.childCount == 0)
                transform.SetParent(GameSceneManager.Instance.EquipmentPage.EquipSlots[Data.Category].transform);
            else
            {
                var obj = GameSceneManager.Instance.EquipmentPage.EquipSlots[Data.Category].transform.GetChild(0);
                obj.SetParent(transform.parent);
                transform.SetParent(GameSceneManager.Instance.EquipmentPage.EquipSlots[Data.Category].transform);
            }
            EventManager.Emit("UpdatePlayerEquip");
        }
        else
        {
            if(GameSceneManager.Instance.UsePotion())
            {
                UpdateData();
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GameSceneManager.Instance.Inventory.ItemDescriptor.text = "";
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GameSceneManager.Instance.Inventory.ItemDescriptor.text = Data.Name +"\n"+ Data.Descript;
    }
}
