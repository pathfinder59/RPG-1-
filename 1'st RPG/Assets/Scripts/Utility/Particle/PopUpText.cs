using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpText : MonoBehaviour
{
    [SerializeField]
    TextMesh _text;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void InitText(string str,Color color )
    {
        _text.text = str;
        _text.color = color;
    }

}
