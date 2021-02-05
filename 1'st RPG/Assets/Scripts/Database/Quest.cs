using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Quest
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
}
