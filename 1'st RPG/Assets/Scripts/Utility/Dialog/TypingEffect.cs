using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TypingEffect : MonoBehaviour
{

    [SerializeField]
    protected Text _text;
    [SerializeField]
    float _timeTerm;
    protected string _str;
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    public IEnumerator OutputText()
    {
        for(int i = 0; i< _str.Length; ++i)
        {
            yield return new WaitForSeconds(_timeTerm);
            _text.text += _str[i];
        }
    }
}
