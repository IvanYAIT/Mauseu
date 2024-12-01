using System.Collections.Generic;
using Shared.DataProvider.Commands;

namespace Services.Weapons.Commands
{
    public class LoadWeaponDataCommand : LoadDataCommand<List<UserWeaponData>>
    {
        protected override List<UserWeaponData> GetDefault() => new();

        protected override string GetContainerName() => "UserWeapon.json";
    }
}