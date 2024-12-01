using Services.Inventory.Items;

namespace Services.Weapons.Data
{
    [System.Serializable]
    public class Resource
    {
        public ItemType Type;
        public int Amount;
    }
}