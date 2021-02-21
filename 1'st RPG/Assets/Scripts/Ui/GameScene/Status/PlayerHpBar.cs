using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using common;
public class PlayerHpBar : MonoBehaviour
{
    [SerializeField]
    Text _text;
    [SerializeField]
    Image _guage;
    void Start()
    {
        EventManager.On("UpdateStatus", UpdateInform);
        EventManager.Emit("UpdateStatus");
    }
    private void OnEnable()
    {
        EventManager.Emit("UpdateStatus");
    }
    void Update()
    {
        
    }
    public void UpdateInform(GameObject obj = null)
    {
        int hp = PlayerManager.Instance._playerStat.Hp;
        int maxHp = PlayerManager.Instance._playerStat.MaxHp;
        _guage.fillAmount = (float)hp / maxHp;
        _text.text = string.Format("{0}/{1}", hp, maxHp);
    }
}
