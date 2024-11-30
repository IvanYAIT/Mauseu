using System.Collections.Generic;
using UnityEngine;

namespace PlayerInventory
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private int slotCount;
        [SerializeField] private GameObject monsterIcon;

        private List<Item> _items;
        private bool isMonsterPickedUp;
        private MonsterData _currentMonster;

        private void Awake()
        {
            _items = new List<Item>();
        }

        public bool TakeMonster(MonsterData monster)
        {
            if (_currentMonster == null)
            {
                _currentMonster = monster;
                isMonsterPickedUp = true;
                monsterIcon.SetActive(true);
                return true;
            }
            return false;
        }

        public void DropMonster()
        {
            if(_currentMonster == null)
            {
                _currentMonster = null;
                isMonsterPickedUp = false;
                monsterIcon.SetActive(false);
            }
        }

        public bool PutMonster(out MonsterData monsterData)
        {
            if (_currentMonster == null)
            {
                monsterData = _currentMonster;
                _currentMonster = null;
                isMonsterPickedUp = false;
                monsterIcon.SetActive(false);
                return true;
            }
            monsterData = null;
            return false;
        }

        public bool AddItem(Item itemToAdd)
        {
            if(slotCount < _items.Count)
            {
                _items.Add(itemToAdd);
                return true;
            }
            return false;
        }

        public void RemoveItem(Item itemToRemove)
        {
            if(_items.Contains(itemToRemove))
                _items.Remove(itemToRemove);
        }
    }
}