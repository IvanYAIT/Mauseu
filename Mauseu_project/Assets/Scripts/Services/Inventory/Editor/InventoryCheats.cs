using System;
using Dependencies.ChaserLib.ServiceLocator;
using Services.Inventory.Items;
using Services.Weapons;
using UnityEditor;

namespace Services.Inventory.Editor
{
    public class InventoryCheats : UnityEditor.Editor
    {
        private static ServiceLocator Locator => ServiceLocator.Instance;
        private static IInventoryService InventoryService => Locator.Get<IInventoryService>();
        private static IWeaponService WeaponService => Locator.Get<IWeaponService>();

        [MenuItem("Test/AddTestInventory")]
        public static void AddTestInventory()
        {
            var testWeapon1Id = Guid.NewGuid();
            WeaponService.Add(ItemType.TestWeapon1, 0, testWeapon1Id);

            //var testWeapon2Id = Guid.NewGuid();
            //WeaponService.Add(ItemType.TestWeapon2, 0, testWeapon2Id);

            InventoryService.AddItem(ItemType.TestWeapon1, testWeapon1Id);
            //InventoryService.AddItem(ItemType.TestWeapon2, testWeapon2Id);

            InventoryService.AddItem(ItemType.TestMonster1, Guid.NewGuid());
            InventoryService.AddItem(ItemType.TestMonster2, Guid.NewGuid());
            InventoryService.AddItem(ItemType.TestMonster3, Guid.NewGuid());

            InventoryService.AddItem(ItemType.TestConsumable1, 10);
            InventoryService.AddItem(ItemType.TestConsumable2, 10);

            InventoryService.AddItem(ItemType.TestResource1, 10);
            InventoryService.AddItem(ItemType.TestResource2, 10);
        }
    }
}