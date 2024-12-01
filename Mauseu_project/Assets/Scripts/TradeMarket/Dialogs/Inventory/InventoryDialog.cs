using System.Collections.Generic;
using Dependencies.ChaserLib.Dialogs;
using Dependencies.ChaserLib.ServiceLocator;
using Services.Inventory;
using Services.Inventory.Items;
using TradeMarket.Dialogs.Inventory.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace TradeMarket.Dialogs.Inventory
{
    public class InventoryDialog : DialogBase
    {
        [System.Serializable]
        private struct CategoryToggle
        {
            public ItemCategory Category;
            public Toggle Toggle;
        }

        [SerializeField] private CategoryToggle[] _categories;
        [SerializeField] private ItemView _itemPrefab;
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private ItemsData _itemsData;

        private static ServiceLocator Locator => ServiceLocator.Instance;
        private static IInventoryService InventoryService => Locator.Get<IInventoryService>();

        private readonly Dictionary<ItemCategory, List<ItemView>> _itemsByCategory = new();

        private ItemCategory _currentItemCategory;

        public override void Show()
        {
            foreach (var category in _categories)
            {
                category.Toggle.onValueChanged.AddListener(isOn =>
                {
                    if (!isOn)
                        return;

                    DisableCurrentCategory();
                    ShowCategory(category.Category);
                });
            }

            var stackableItems = InventoryService.GetAllStackableItems();
            var uniqItems = InventoryService.GetAllUniqItems();

            _itemsByCategory.Add(ItemCategory.Weapon, new List<ItemView>());
            _itemsByCategory.Add(ItemCategory.Consumables, new List<ItemView>());
            _itemsByCategory.Add(ItemCategory.Monsters, new List<ItemView>());
            _itemsByCategory.Add(ItemCategory.Resources, new List<ItemView>());

            foreach (var item in stackableItems)
            {
                InstantiateItem(item.Key, item.Value);
            }

            foreach (var item in uniqItems)
            {
                InstantiateItem(item.Type);
            }

            ShowCategory(ItemCategory.Weapon);
            base.Show();
        }

        private void InstantiateItem(ItemType type, int amount = 1)
        {
            var data = _itemsData.GetItemData(type);
            var instance = Instantiate(_itemPrefab, _itemsContainer);
            instance.SetData(data.Name, data.Description, data.Icon, amount);
            instance.gameObject.SetActive(false);

            _itemsByCategory[data.Category].Add(instance);
        }

        private void DisableCurrentCategory()
        {
            SetActiveCategory(_currentItemCategory, false);
        }

        private void ShowCategory(ItemCategory newCategory)
        {
            _currentItemCategory = newCategory;
            SetActiveCategory(newCategory, true);
        }

        private void SetActiveCategory(ItemCategory category, bool state)
        {
            var items = _itemsByCategory[category];
            foreach (var item in items)
            {
                item.gameObject.SetActive(state);
            }
        }
    }
}