using Services.Inventory.Items;
using UnityEngine;

namespace Services.TradeMarket.Data
{
    [System.Serializable]
    public class ItemPriceData
    {
        public ItemType Type;
        public int Cost;
        [Range(0, 10)] public int Valuability;
    }
}