using System;
using Dependencies.ChaserLib.ServiceLocator;
using Services.Inventory;
using Services.Wallet;
using Services.Weapons;
using Services.Weapons.Data;

namespace Services.Forge
{
    public class ForgeService : IForgeService
    {
        private static ServiceLocator Locator => ServiceLocator.Instance;
        private static IWeaponService WeaponService => Locator.Get<IWeaponService>();
        private static IInventoryService InventoryService => Locator.Get<IInventoryService>();
        private static IWalletService WalletService => Locator.Get<IWalletService>();

        public void Upgrade(Guid id)
        {
            var upgradeCost = GetUpgradeCost(id);
            WalletService.Remove(upgradeCost.Cost);

            foreach (var resource in upgradeCost.Resources)
            {
                InventoryService.RemoveItem(resource.Type, resource.Amount);
            }
            
            WeaponService.Upgrade(id);
        }

        public bool CanUpgrade(Guid id, int level)
        {
            if (WeaponService.IsMaxLevel(id))
                return false;
            
            var upgradeCost = GetUpgradeCost(id);

            if (WalletService.Get() < upgradeCost.Cost)
                return false;

            var resources = upgradeCost.Resources;
            
            foreach (var resource in resources)
            {
                var currentAmount = InventoryService.GetAmount(resource.Type);
                if (currentAmount < resource.Amount)
                    return false;
            }

            return true;
        }

        private static UpgradeCost GetUpgradeCost(Guid id)
        {
            var type = WeaponService.GetById(id).Type;
            var currentLevel = WeaponService.GetCurrentLevel(id);
            return WeaponService.GetUpgradeCost(type, currentLevel + 1);
        }
    }
}