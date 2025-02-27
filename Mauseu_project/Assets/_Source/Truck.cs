﻿using Dependencies.ChaserLib.ServiceLocator;
using PlayerInventory;
using Services.Inventory;
using Services.Inventory.Items;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Truck : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private string _hubSceneName;
    
    private readonly List<ItemType> _collectedMonsters = new();
    
    private static ServiceLocator Locator;
    private static IInventoryService InventoryService;
    
    private Inventory _playerInventory;
    private int _playerLayre;

    private void Start()
    {
        _playerLayre = (int)Mathf.Log(playerLayerMask.value, 2);
        Locator = ServiceLocator.Instance;
        InventoryService = Locator.Get<IInventoryService>();
    }

    private void Update()
    {
        if(_playerInventory != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(_playerInventory.PutMonster(out var currentMosnter))
                {
                    _collectedMonsters.Add(currentMosnter);
                }
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                foreach (var monster in _collectedMonsters)
                {
                    Debug.Log(monster);
                    InventoryService.AddItem(monster, Guid.NewGuid());
                }
                
                SceneManager.LoadScene(_hubSceneName);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == _playerLayre)
        {
            _playerInventory = other.gameObject.GetComponent<Inventory>();  
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == _playerLayre)
        {
            _playerInventory = null;
        }
    }
}
