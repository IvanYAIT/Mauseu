using System.Collections.Generic;
using Dependencies.ChaserLib.Dialogs;
using Dependencies.ChaserLib.ServiceLocator;
using Services.Inventory;
using Services.Inventory.Commands;
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

            var items = InventoryService.GetAllItems();
            
            _itemsByCategory.Add(ItemCategory.Weapon, new List<ItemView>());
            _itemsByCategory.Add(ItemCategory.Consumables, new List<ItemView>());
            _itemsByCategory.Add(ItemCategory.Monsters, new List<ItemView>());
            
            foreach (var item in items)
            {
                var data = _itemsData.GetItemData(item.Key);
                var instance = Instantiate(_itemPrefab, _itemsContainer);
                instance.SetData(data.Name, data.Description, data.Icon, item.Value);
                instance.gameObject.SetActive(false);
                
                _itemsByCategory[data.Category].Add(instance);
            }
            
            ShowCategory(ItemCategory.Weapon);
            base.Show();
        }

        public override void Hide()
        {
            new SaveInventoryDataCommand(InventoryService.GetAllItems()).Execute();
            base.Hide();
        }

        private void DisableCurrentCategory()
        {
            var items = _itemsByCategory[_currentItemCategory];
            foreach (var item in items)
            {
                item.gameObject.SetActive(false);
            }
        }

        private void ShowCategory(ItemCategory newCategory)
        {
            _currentItemCategory = newCategory;
            var items = _itemsByCategory[newCategory];
            foreach (var item in items)
            {
                item.gameObject.SetActive(true);
            }
        }
    }
}