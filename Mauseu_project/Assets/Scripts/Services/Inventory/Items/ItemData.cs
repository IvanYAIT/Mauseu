﻿using UnityEngine;

namespace Services.Inventory.Items
{
    [System.Serializable]
    public class ItemData
    {
        public string Name;
        public string Description;
        public Sprite Icon;
        public ItemType Type;
        public ItemCategory Category;
        public bool IsStackable;
    }
}