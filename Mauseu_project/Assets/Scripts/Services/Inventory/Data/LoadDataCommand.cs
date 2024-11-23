using System.Collections.Generic;
using System.IO;
using FrostLib.Commands;
using Newtonsoft.Json;
using Services.Inventory.Items;
using UnityEngine;

namespace Services.Inventory.Data
{
    public class LoadDataCommand : ICommand<Dictionary<ItemType, int>>
    {
        private const string ContainerName = "Inventory.json";

        public Dictionary<ItemType, int> Execute()
        {
            var path = Path.Join(Application.persistentDataPath, ContainerName);
            return JsonConvert.DeserializeObject<Dictionary<ItemType, int>>(path);
        }
    }
}