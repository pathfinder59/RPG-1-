﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Npc : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    Image questImage;
    [SerializeField]
    Sprite questStart;
    [SerializeField]
    Sprite questReady;
    [SerializeField]
    Sprite questFinish;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetQuestImage(int pattern)
    {
        switch(pattern)
        {
            case 1:
                questImage.sprite = questStart;
                break;
            case 2:
                questImage.sprite = questReady;
                break;
            case 3:
                questImage.sprite = questFinish;
                break;
        }
    }
}
