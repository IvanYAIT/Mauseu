using System;

namespace Services.Inventory.Items
{
    public struct UniqItem
    {
        public ItemType Type;
        public Guid Id;

        public UniqItem(ItemType type, Guid id)
        {
            Type = type;
            Id = id;
        }
    }
}