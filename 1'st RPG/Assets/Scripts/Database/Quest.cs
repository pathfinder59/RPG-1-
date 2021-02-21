using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public int client;
    public int targetId;
    public int num;
    public int processRate;
    public Quest(QuestData data,bool isClear = false)
    { 
        client = data.client;
        targetId = data.target;
        num = data.num;
         
        processRate = isClear ? 3 : 2;
    }

    public void DecreaseNum(int n)
    {
        num = Mathf.Clamp(num-n,0,1000);
        if (num == 0)
        {
            GameObject.Find("Environment").transform.Find("Npcs").Find(client.ToString()).GetComponent<Npc>().SetQuestImage(3);
            processRate = 3;
        }  
    }
}
