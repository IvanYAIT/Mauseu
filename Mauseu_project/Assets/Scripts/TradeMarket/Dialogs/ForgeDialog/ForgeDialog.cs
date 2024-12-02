using System;
using System.Collections.Generic;
using System.Linq;
using Dependencies.ChaserLib.Dialogs;
using Dependencies.ChaserLib.ServiceLocator;
using Services.Forge;
using Services.Inventory;
using Services.Inventory.Items;
using Services.Wallet;
using Services.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TradeMarket.Dialogs.ForgeDialog
{
    public class ForgeDialog : DialogBase
    {
        [SerializeField] private WeaponItem _prefab;
        [SerializeField] private Transform _container;
        [SerializeField] private UpgradeView _upgradeView;
        [SerializeField] private ItemsData _itemsData;
        [SerializeField] private TMP_Text _walletLabel;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private ResourceView[] _userResources;

        private static ServiceLocator Locator => ServiceLocator.Instance;
        private static IWeaponService WeaponService => Locator.Get<IWeaponService>();
        private static IInventoryService InventoryService => Locator.Get<IInventoryService>();
        private static IWalletService WalletService => Locator.Get<IWalletService>();
        private static IForgeService ForgeService => Locator.Get<IForgeService>();

        private readonly List<WeaponItem> _weaponInstances = new();
        private Guid _selectedId;

        public override void Show()
        {
            UpdateUserResource();
            
            var userWeapon = WeaponService.GetAllUserWeapon();

            foreach (var weaponData in userWeapon)
            {
                var instance = Instantiate(_prefab, _container);
                var itemData = _itemsData.GetItemData(weaponData.Type);
                var currentLevel = WeaponService.GetCurrentLevel(weaponData.Id);

                instance.SetData(itemData.Icon, itemData.Name, currentLevel, weaponData.Id);
                instance.OnItemClick.AddListener(SelectWeapon);

                _weaponInstances.Add(instance);
            }
        
            SelectWeapon(userWeapon.First().Id);
            
            base.Show();
        }
        
        private void SelectWeapon(Guid id)
        {
            var weaponData = WeaponService.GetById(id);
            var current = WeaponService.GetCharacteristic(weaponData.Type, weaponData.Level);

            var canUpgrade = ForgeService.CanUpgrade(id);
            var isMaxLevel = WeaponService.IsMaxLevel(id);
            _upgradeButton.interactable = canUpgrade;

            var upgrade = isMaxLevel
                ? current
                : WeaponService.GetCharacteristic(weaponData.Type, weaponData.Level + 1);
            var upgradeCost =
                WeaponService.GetUpgradeCost(weaponData.Type, isMaxLevel ? 0 : weaponData.Level + 1);

            _upgradeView.SetData(current, upgrade, upgradeCost);
            _selectedId = id;
        }

        public override void Hide()
        {
            foreach (var instance in _weaponInstances)
            {
                instance.OnItemClick.ClearListeners();
            }

            base.Hide();
        }

        public void Upgrade()
        {
            if (!ForgeService.CanUpgrade(_selectedId))
                return;

            ForgeService.Upgrade(_selectedId);
            SelectWeapon(_selectedId);
            UpdateViews();
        }

        private void UpdateViews()
        {
            foreach (var instance in _weaponInstances)
            {
                var level = WeaponService.GetCurrentLevel(instance.Id);
                instance.SetLevel(level);
            }

            UpdateUserResource();
        }
        
        private void UpdateUserResource()
        {
            _walletLabel.text = WalletService.Get().ToString();

            foreach (var resource in _userResources)
            {
                var amount = InventoryService.GetAmount(resource.Type);
                resource.AmountLabel.text = amount.ToString();

                var icon = _itemsData.GetItemData(resource.Type).Icon;
                resource.Icon.sprite = icon;
            }
        }
    }
}