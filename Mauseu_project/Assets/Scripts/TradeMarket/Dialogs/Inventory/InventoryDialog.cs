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
        
        private static ServiceLocator Locator => ServiceLocator.Instance;
        private static IInventoryService InventoryService => Locator.Get<IInventoryService>();

        private Dictionary<CategoryType, List<ItemView>> _itemsByCategory;

        public override void Show()
        {
            foreach (var category in _categories)
            {
                category.Toggle.onValueChanged.AddListener(isOn =>
                {
                    if(!isOn)
                        return;

                    DisableCurrentCategory();
                    ShowCategory(category.Category);
                });
            }
            
            var items = InventoryService.GetAllItems();
            
           
        
            base.Show();
        }

        public override void Hide()
        {
            new SaveDataCommand(InventoryService.GetAllItems()).Execute();
            base.Hide();
        }

        private void DisableCurrentCategory()
        {
            
        }
        
        private void ShowCategory(CategoryType type)
        {
        
        }
    }
}
