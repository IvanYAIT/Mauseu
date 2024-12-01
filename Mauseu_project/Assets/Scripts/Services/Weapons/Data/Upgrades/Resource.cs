using Services.Inventory.Items;

namespace Services.Weapons.Data
{
    [System.Serializable]
    public struct Resource
    {
        public ItemType Type;
        public int Amount;
    }
}