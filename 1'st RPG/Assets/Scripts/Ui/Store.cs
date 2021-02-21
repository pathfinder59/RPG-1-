using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class Store : Obj
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }  

    public override void Interact()
    {
        UiController.Instance.OpenStorePage(true);
       // GameSceneManager.Instance.SetStorePageActive(true);
       // GameSceneManager.Instance.SetPlayUiActive(false);
    }
}
