using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using common;
public class QuestManager : MonoBehaviour
{
    public Dictionary<int,Quest>[] currentQuests;
    private void Awake()
    {

        currentQuests = new Dictionary<int, Quest>[3];
        for(int i = 0; i<3; ++i)
            currentQuests[i] = new Dictionary<int, Quest>();
        
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Init()
    {

    }
}
