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

    Material mat;
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
            ActionObj.GetComponent<Obj>().Interact();
        
        gameObject.SetActive(false);
    }
}
