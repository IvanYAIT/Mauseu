using Dependencies.ChaserLib.ServiceLocator;
using Services.Inventory.Items;
using UnityEditor;

namespace Services.Inventory.Editor
{
    public class InventoryCheats : UnityEditor.Editor
    {
        private static ServiceLocator Locator => ServiceLocator.Instance;
        private static IInventoryService InventoryService => Locator.Get<IInventoryService>();

        [MenuItem("Test/AddWeaponsToInventory%1")]
        public static void AddWeapon()
        {
            InventoryService.AddItem(ItemType.TestWeapon1, 1);
            InventoryService.AddItem(ItemType.TestWeapon2, 1);
        }

        [MenuItem("Test/AddConsumablesToInventory%2")]
        public static void AddConsumable()
        {
            InventoryService.AddItem(ItemType.TestConsumable1, 10);
            InventoryService.AddItem(ItemType.TestConsumable2, 10);
        }
    
        [MenuItem("Test/AddMonstersToInventory%3")]
        public static void AddMonsters()
        {
            InventoryService.AddItem(ItemType.TestMonster1, 1);
            InventoryService.AddItem(ItemType.TestMonster2, 2);
        }
    }
}