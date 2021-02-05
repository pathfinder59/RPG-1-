using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public int targetId;
    public int num;
    public int processRate;
    public Quest(QuestData data)
    {
        targetId = data.target;
        num = data.num;
        processRate = 2;
    }

    public void DecreaseNum(int n)
    {
        num = Mathf.Clamp(num-n,0,1000);
        if (num == 0)
            processRate = 3;
            
    }
}
