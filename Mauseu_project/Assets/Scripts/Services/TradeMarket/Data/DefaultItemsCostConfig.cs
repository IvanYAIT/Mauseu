using UnityEngine;

namespace Services.TradeMarket.Data
{
    [CreateAssetMenu(fileName = "ItemsCostConfig", menuName = "Data/ItemsCostConfig", order = 3)]
    public class DefaultItemsCostConfig : ScriptableObject
    {
        [SerializeField] private ItemPriceData[] _items;

        public ItemPriceData[] GetAllItems() => _items;
    }
}