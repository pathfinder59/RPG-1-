using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlotList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public virtual bool AddItem(ItemData data, GameObject itemUi)
    {
        return true;
    }
    public virtual void SubItem(int category)
    {
        
    }
}
