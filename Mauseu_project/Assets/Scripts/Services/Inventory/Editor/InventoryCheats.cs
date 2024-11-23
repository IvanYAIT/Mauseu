using Dependencies.ChaserLib.ServiceLocator;
using Services.Inventory;
using Services.Inventory.Data;
using Services.Inventory.Items;
using UnityEditor;

public class InventoryCheats : Editor
{
    private static ServiceLocator Locator => ServiceLocator.Instance;
    private static IInventoryService InventoryService => Locator.Get<IInventoryService>();

    [MenuItem("Test/AddWeaponsToInventory%1")]
    public static void AddWeapon()
    {
        InventoryService.AddItem(ItemType.TestWeapon1, 1);
        InventoryService.AddItem(ItemType.TestWeapon2, 1);
        new SaveDataCommand(InventoryService.GetAllItems()).Execute();
    }

    [MenuItem("Test/AddConsumablesToInventory%2")]
    public static void AddConsumable()
    {
        InventoryService.AddItem(ItemType.TestConsumable1, 10);
        InventoryService.AddItem(ItemType.TestConsumable2, 10);
        new SaveDataCommand(InventoryService.GetAllItems()).Execute();
    }
    
    [MenuItem("Test/AddMonstersToInventory%3")]
    public static void AddMonsters()
    {
        InventoryService.AddItem(ItemType.TestMonster1, 1);
        InventoryService.AddItem(ItemType.TestMonster2, 2);
        new SaveDataCommand(InventoryService.GetAllItems()).Execute();
    }
}