using System.Collections.Generic;
using Dependencies.ChaserLib.Dialogs;
using Dependencies.ChaserLib.ServiceLocator;
using Services.Inventory;
using Services.Inventory.Data;
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
            public CategoryType Category;
            public Toggle Toggle;
        }

        [SerializeField] private CategoryToggle[] _categories;
        [SerializeField] private ItemView _itemPrefab;
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private ItemsData _itemsData;

        private static ServiceLocator Locator => ServiceLocator.Instance;
        private static IInventoryService InventoryService => Locator.Get<IInventoryService>();

        private readonly Dictionary<CategoryType, List<ItemView>> _itemsByCategory = new();
        
        private CategoryType _currentCategory;
        
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
            
            _itemsByCategory.Add(CategoryType.Weapon, new List<ItemView>());
            _itemsByCategory.Add(CategoryType.Consumables, new List<ItemView>());
            _itemsByCategory.Add(CategoryType.Monsters, new List<ItemView>());
            
            foreach (var item in items)
            {
                var data = _itemsData.GetItemData(item.Key);
                var instance = Instantiate(_itemPrefab, _itemsContainer);
                instance.SetData(data.Name, data.Description, data.Icon, item.Value);
                instance.gameObject.SetActive(false);
                
                _itemsByCategory[data.Category].Add(instance);
            }
            
            ShowCategory(CategoryType.Weapon);
            base.Show();
        }

        public override void Hide()
        {
            new SaveDataCommand(InventoryService.GetAllItems()).Execute();
            base.Hide();
        }

        private void DisableCurrentCategory()
        {
            var items = _itemsByCategory[_currentCategory];
            foreach (var item in items)
            {
                item.gameObject.SetActive(false);
            }
        }

        private void ShowCategory(CategoryType newCategory)
        {
            _currentCategory = newCategory;
            var items = _itemsByCategory[newCategory];
            foreach (var item in items)
            {
                item.gameObject.SetActive(true);
            }
        }
    }
}