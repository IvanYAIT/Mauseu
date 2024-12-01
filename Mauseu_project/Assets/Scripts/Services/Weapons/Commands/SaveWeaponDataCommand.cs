using System.Collections.Generic;
using Shared.DataProvider.Commands;

namespace Services.Weapons.Commands
{
    public class SaveWeaponDataCommand : SaveDataCommand<List<UserWeaponData>>
    {
        public SaveWeaponDataCommand(List<UserWeaponData> data) : base(data)
        {
        }

        protected override string GetContainerName() => "UserWeapon.json";
    }
}