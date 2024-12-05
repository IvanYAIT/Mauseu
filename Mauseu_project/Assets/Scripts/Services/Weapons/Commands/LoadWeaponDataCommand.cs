using System;
using System.Collections.Generic;
using Services.Inventory.Items;
using Shared.DataProvider.Commands;

namespace Services.Weapons.Commands
{
    public class LoadWeaponDataCommand : LoadDataCommand<List<UserWeaponData>>
    {
        protected override List<UserWeaponData> GetDefault()
        {
            var userWeapon = new List<UserWeaponData>();
            var newWeapon = new UserWeaponData(ItemType.TestWeapon1, 0, Guid.NewGuid());
            userWeapon.Add(newWeapon);
            return userWeapon;
        }

        protected override string GetContainerName() => "UserWeapon.json";
    }
}