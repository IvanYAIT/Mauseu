using Monster;
using Services.Inventory.Items;
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
        private ItemType _currentMonster;

        private void Awake()
        {
            _items = new List<Item>();
        }

        public bool TakeMonster(ItemType monster)
        {
            if (!isMonsterPickedUp)
            {
                _currentMonster = monster;
                isMonsterPickedUp = true;
                monsterIcon.SetActive(true);
                isMonsterPickedUp = true;
                return true;
            }
            return false;
        }

        public void DropMonster()
        {
            if(isMonsterPickedUp)
            {
                isMonsterPickedUp = false;
                monsterIcon.SetActive(false);
            }
        }

        public bool PutMonster(out ItemType monsterData)
        {
            if (isMonsterPickedUp)
            {
                monsterData = _currentMonster;
                isMonsterPickedUp = false;
                monsterIcon.SetActive(false);
                return true;
            }
            monsterData = ItemType.TestMonster2;
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