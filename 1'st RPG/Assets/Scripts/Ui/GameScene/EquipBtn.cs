using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using common;

public class EquipBtn : MonoBehaviour
{
    [SerializeField]
    InventoryPage _page;
    public ItemData Data { get; set; }

    void Start()
    {
        Data = null;
        EventManager.On("UpdateInventory", UpdateInform);
    }

    void Update()
    {
        
    }

    void OnClick()
    {
        if (Data == null)
            return;
        if (_page.AddItem(Data))
        {
            Data = null;
            GetComponent<Button>().image.sprite = null;
            EventManager.Emit("UpdateInventory", null);
        }
    }

    void UpdateInform(GameObject obj = null)
    {
        Button btn = GetComponent<Button>();
        btn.image.sprite = (Data == null ? null : Data.Sprite);
    }
}
