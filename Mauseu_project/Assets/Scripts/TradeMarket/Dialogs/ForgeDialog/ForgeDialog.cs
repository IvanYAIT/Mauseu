using System;
using System.Collections.Generic;
using Dependencies.ChaserLib.Dialogs;
using Dependencies.ChaserLib.ServiceLocator;
using Services.Inventory;
using Services.Inventory.Items;
using Services.Weapons;
using UnityEngine;

namespace TradeMarket.Dialogs.ForgeDialog
{
    public class ForgeDialog : DialogBase
    {
        [SerializeField] private WeaponItem _prefab;
        [SerializeField] private Transform _container;
        [SerializeField] private ItemsData _itemsData;

        private static ServiceLocator Locator => ServiceLocator.Instance;
        private static IWeaponService WeaponService => Locator.Get<IWeaponService>();

        public List<WeaponItem> _instances;

        public override void Show()
        {
            var userWeapon = WeaponService.GetAllUserWeapon();

            foreach (var weaponData in userWeapon)
            {
                var instance = Instantiate(_prefab, _container);
                var itemData = _itemsData.GetItemData(weaponData.Type);
                var currentLevel = WeaponService.GetCurrentLevel(weaponData.Id);
                
                instance.SetData(itemData.Icon, itemData.Name, currentLevel, weaponData.Id);
                instance.OnItemClick.AddListener(SelectWeapon);
                
                _instances.Add(instance);
            }

            base.Show();
        }

        private void SelectWeapon(Guid id)
        {
        }

        public override void Hide()
        {
            foreach (var instance in _instances)
            {
                instance.OnItemClick.ClearListeners();
            }

            base.Hide();
        }
    }
}