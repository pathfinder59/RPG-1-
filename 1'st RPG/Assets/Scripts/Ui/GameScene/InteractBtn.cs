using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractBtn : MonoBehaviour
{
    [SerializeField]
    Canvas StoreCanvas;
    [SerializeField]
    Canvas GameSceneCanvas;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ClickBtn()
    {
        if (GameSceneManager.Instance.isSetting)
            return;

        if(DialogManager.Instance.ActionObject.layer == LayerMask.NameToLayer("Npc"))
        {
            DialogManager.Instance.Interact();
        }
        else if(DialogManager.Instance.ActionObject.layer == LayerMask.NameToLayer("Store"))
        {
            StoreCanvas.gameObject.SetActive(true);
            GameSceneCanvas.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
