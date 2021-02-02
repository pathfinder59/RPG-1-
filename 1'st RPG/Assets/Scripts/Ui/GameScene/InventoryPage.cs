using common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryPage : MonoBehaviour
{
    [SerializeField]
    List<InventoryBtn> gameObjects;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool AddItem(ItemData data)
    {
        var item = gameObjects.FirstOrDefault(_ => _.Data == null);
        if (item == null)
            return false;

        item.Data = data;
              
        return true;
    }
}
