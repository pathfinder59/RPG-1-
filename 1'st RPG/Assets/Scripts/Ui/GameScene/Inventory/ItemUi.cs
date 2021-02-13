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
    public static Transform PrevSlot;
    public ItemData Data { get; set; }
    [SerializeField]
    Image _image;

    Transform InventoryPage;
    
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
    }

}
