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
        EventManager.On("UpdatePlayerEquip", UpdateInform);
    }

    void Update()
    {
        
    }

    public void OnClick()
    {
        if (Data == null)
            return;
        if (_page.AddItem(Data))
        {
            Data = null;
            GetComponent<Button>().image.sprite = null;
            EventManager.Emit("UpdateInventory", null);
            EventManager.Emit("UpdatePlayerEquip", null);
        }
    }

    public void UpdateInform(GameObject obj = null)
    {
        Button btn = GetComponent<Button>();
        btn.image.sprite = (Data == null ? null : Data.Sprite);
    }
}
