using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogPage : TypingEffect
{
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    public void SetDlg(string content)
    {
        if (content == _str)
        {
            StopAllCoroutines();
            _text.text = content;
            DialogManager.Instance.UpIdx();
        }
        else
        {
            StartCoroutine(SetTextContent(content));
        }
    }
    public IEnumerator SetTextContent(string content)
    {
        _text.text = "";
        _str = content;
        yield return StartCoroutine(OutputText());
        DialogManager.Instance.UpIdx();
        yield break;
    }
}
