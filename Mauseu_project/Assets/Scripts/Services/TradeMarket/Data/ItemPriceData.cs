using Services.Inventory.Items;
using UnityEngine;

namespace Services.TradeMarket.Data
{
    [System.Serializable]
    public class ItemPriceData
    {
        public ItemType Type;
        public ItemCategory Category;
        public int Cost;
        [Range(1, 10)] public int Valuability;
        public bool HasFlexiblePrice;

        public ItemPriceData Clone()
        {
            return new ItemPriceData
            {
                Type = Type,
                Category = Category,
                Cost = Cost,
                Valuability = Valuability,
                HasFlexiblePrice = HasFlexiblePrice
            };
        }
    }
}