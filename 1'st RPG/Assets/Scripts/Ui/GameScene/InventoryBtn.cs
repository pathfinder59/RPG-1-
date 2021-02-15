using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBtn : MonoBehaviour,ICommander
{
    [SerializeField]
    InventoryPage InventoryPage;
    [SerializeField]
    GameObject InventoryList;
    public void Off()
    {
        InventoryList.SetActive(false);
    }

    public void On()
    {
        InventoryList.SetActive(true);
    }

    public void OnClick()
    {
        InventoryPage.ClickPageBtn(this);
    }


    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
