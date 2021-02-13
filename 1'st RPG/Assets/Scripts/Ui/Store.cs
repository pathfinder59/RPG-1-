using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
public class Store : MonoBehaviour
{
    [SerializeField]
    Button storeEnterBtn;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        //storeEnterBtn.gameObject.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
       // storeEnterBtn.gameObject.SetActive(false);
    }
}
