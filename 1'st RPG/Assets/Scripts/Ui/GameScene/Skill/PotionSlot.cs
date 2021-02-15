using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using common;
public class PotionSlot : MonoBehaviour
{
    [SerializeField]
    Text _text;
    [SerializeField]
    float coolDown;

    float curTime;

    PlayerStat playerStat;
    [SerializeField]
    SupplySlotList supplySlotList;
    private void Awake()
    {
        curTime = 0;
        EventManager.On("UpdatePotion", UpdatePotion);

    }
    void Start()
    {
        _text.text = PlayerManager.Instance.NumHp.ToString();
        playerStat = GameObject.Find("Player").GetComponent<PlayerStat>();
    }

    void Update()
    {
        curTime = Mathf.Clamp(curTime - Time.deltaTime, 0, coolDown);

        GetComponent<Button>().image.fillAmount = 1 - (curTime / coolDown);
    }

    public void UpdatePotion(GameObject obj = null)
    {
        _text.text = PlayerManager.Instance.NumHp.ToString();
    }

    public void OnClick()
    {
        UsePotion();
    }

    public bool UsePotion()
    {
        if (PlayerManager.Instance.NumHp == 0)
            return false;
        if (curTime == 0)
        {
            PlayerManager.Instance.NumHp--;
            //플레이어 회복
            playerStat.Heal(50);
            curTime = coolDown;
            supplySlotList.SubItem(100);
            UpdatePotion();
            return true;
        }
        return false;
    }
}
