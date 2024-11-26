using UnityEngine;

namespace Services.TradeMarket.Data
{
    [CreateAssetMenu(fileName = "ItemsCostConfig", menuName = "Data/ItemsCostConfig", order = 3)]
    public class DefaultItemsCostConfig : ScriptableObject
    {
        [SerializeField] private ItemPriceData[] _items;

        public ItemPriceData[] GetAllItems()
        {
            var targetItems = new ItemPriceData[_items.Length];

            for (var i = 0; i < _items.Length; i++)
            {
                targetItems[i] = _items[i].Clone();
            }

            return targetItems;
        }
    }
}