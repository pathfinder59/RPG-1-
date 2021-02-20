using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemSlotList : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public abstract bool AddItem(ItemData data, GameObject itemUi);
    public abstract void PointerClickItem(Item item);
    public virtual void SubItem(int category)
    {
        
    }
}
