using common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    List<InventoryBtn> gameObjects;
    private void Awake()
    {
    }
    private void OnEnable()
    {
        EventManager.Emit("UpdateInventory");
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool AddItem(ItemData data)
    {
        var item = gameObjects.FirstOrDefault(_ => _.Data == null)?? null;
        if (item == null)
            return false;

        item.Data = data;
        return true;
    }
}
