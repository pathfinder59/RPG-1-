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
        if (GameSceneManager.Instance.IsAct)
            return;
        GameObject ActionObj = GameSceneManager.Instance.ActionObject;

        if (ActionObj.layer == LayerMask.NameToLayer("Npc"))
        {
            DialogManager.Instance.Interact(ActionObj);
        }
        else if(ActionObj.layer == LayerMask.NameToLayer("Store"))
        {
            StoreCanvas.gameObject.SetActive(true);
            GameSceneCanvas.gameObject.SetActive(false);
        }
        else if (ActionObj.layer == LayerMask.NameToLayer("Reinforce"))
        {
            //StoreCanvas.gameObject.SetActive(true);
            //GameSceneCanvas.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}
