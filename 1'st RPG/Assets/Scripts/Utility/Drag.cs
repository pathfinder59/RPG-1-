using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
public class Drag : MonoBehaviour,IDragHandler,IBeginDragHandler,IEndDragHandler
{
    public static GameObject DragedObject;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //transform.SetParent(this.gameObject);
        DragedObject = transform.gameObject;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        DragedObject = null;
    }
}
