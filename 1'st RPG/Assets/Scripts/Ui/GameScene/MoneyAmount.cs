using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using common;
public class MoneyAmount : MonoBehaviour
{
    [SerializeField]
    Text _text;
    void Start()
    {
        EventManager.On("UpdateMoney", UpdateMoney);
        _text.text = PlayerManager.Instance.Money.ToString();
    }
    void Update()
    {
        
    }

    public void UpdateMoney(GameObject obj = null)
    {
        _text.text = PlayerManager.Instance.Money.ToString();
    }
}
