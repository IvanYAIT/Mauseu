using PlayerInventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Truck : MonoBehaviour
{
    [SerializeField] private LayerMask playerLayerMask;

    private List<MonsterData> _collectedMonsters;
    private Inventory _playerInventory;
    private int _playerLayre;
    
    void Start()
    {
        _playerLayre = (int)Mathf.Log(playerLayerMask.value, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerInventory != null)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                MonsterData currentMosnter;
                if(_playerInventory.PutMonster(out currentMosnter))
                {
                    _collectedMonsters.Add(currentMosnter);
                }
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
