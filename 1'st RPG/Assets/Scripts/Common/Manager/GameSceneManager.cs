﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using common;
using GameScene.Ui;

public class GameSceneManager : Singleton<GameSceneManager>
{
    //씬이 변경되어도 이 객체는 항상 값을 유지할 필요가 있음 
    [SerializeField]
    InventoryPage inventoryPage;
    [SerializeField]
    EquipmentPage equipmentPage;

    public GameObject _interactBtn;

    public string playerClass;

    public InventoryPage Inventory => inventoryPage;

    float spawnTime;

    GameObject _actionObject;
    public GameObject ActionObject { get { return _actionObject; } set { _actionObject = value; } }


    bool _isAct;
    public bool IsAct => _isAct;

    void Start()
    {
        Init();
    }

    void Update()
    {
        spawnTime += Time.deltaTime;
        if (spawnTime >= 8f)
        {
            spawnTime = 0.0f;
            EventManager.Emit("Respawn", null);
        }
    }


    void Init()
    {
        _isAct = false;
        _actionObject = null;
        spawnTime = 5.0f;
        playerClass = "Warrior"; // 완성시에는 이 값은 캐릭터 선택화면에서 결정할것

        inventoryPage.gameObject.SetActive(true);
        inventoryPage.gameObject.SetActive(false);
        equipmentPage.gameObject.SetActive(true);
        equipmentPage.gameObject.SetActive(false);
    }

    public void CheckActionObj()
    {
        if (ActionObject == null)
            _interactBtn.SetActive(false);
        else if (ActionObject.layer == LayerMask.NameToLayer("Npc") || ActionObject.layer == LayerMask.NameToLayer("Store"))
           _interactBtn.SetActive(true);
    }
    public void SetIsAct(bool value)
    {
        _isAct = value;
    }
}
