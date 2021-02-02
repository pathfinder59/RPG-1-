using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using common;
public class InventoryBtn : MonoBehaviour
{
    [SerializeField]
    EquipmentPage equipmentPage;

    
    public ItemData Data{get;set;}

    void Start()
    {
        Data = null;
        EventManager.On("UpdateInventory", UpdateInform);
    }

    void Update()
    {
        
    }

    public void OnClick()
    {
        if (Data == null) return;

        EquipBtn btn = equipmentPage.EquipUis[Data.Category];
        if(btn.Data == null)
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
    }

    public void UpdateInform(GameObject obj = null)
    {
        Button btn = GetComponent<Button>();
        btn.image.sprite = (Data == null?  null : Data.Sprite);
    }

    
}
