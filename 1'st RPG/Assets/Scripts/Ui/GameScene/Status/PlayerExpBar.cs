using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using common;
public class PlayerExpBar : MonoBehaviour
{
    [SerializeField]
    Text _text;
    [SerializeField]
    Image _guage;
    void Start()
    {
        EventManager.On("UpdateStatus", UpdateInform);
    }

    void Update()
    {
        
    }

    public void UpdateInform(GameObject obj = null)
    {
        float exp = PlayerManager.Instance._playerStat.Exp;
        float maxExp = PlayerManager.Instance._playerStat.MaxExp;
        _guage.fillAmount = exp / maxExp;
        _text.text = string.Format("{0}/{1}", exp, maxExp);
    }
}
