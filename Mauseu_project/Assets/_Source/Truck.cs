using Dependencies.ChaserLib.ServiceLocator;
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

    private List<ItemType> _collectedMonsters;
    private Inventory _playerInventory;
    private int _playerLayre;
    private IInventoryService _inventory;

    void Start()
    {
        _inventory = ServiceLocator.Instance.Get<IInventoryService>();
        _playerLayre = (int)Mathf.Log(playerLayerMask.value, 2);
    }

    void Update()
    {
        if(_playerInventory != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                ItemType currentMosnter;
                if(_playerInventory.PutMonster(out currentMosnter))
                {
                    _collectedMonsters.Add(currentMosnter);
                }
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                foreach (var monster in _collectedMonsters)
                {
                    _inventory.AddItem(monster, Guid.NewGuid());
                }
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
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
