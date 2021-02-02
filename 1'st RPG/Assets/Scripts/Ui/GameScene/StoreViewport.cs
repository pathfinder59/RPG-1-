using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreViewport : MonoBehaviour
{
    [SerializeField]
    GameObject _prefab;
    [SerializeField]
    List<ItemData> _datas;
    void Start()
    {
        foreach(ItemData data in _datas )
        {
            var go = Instantiate(_prefab);
            go.transform.SetParent(transform);
            go.GetComponent<StoreItemContent>().data = data;
            go.GetComponent<StoreItemContent>().UpdateInform();
        }
    }

    void Update()
    {
        
    }
}
