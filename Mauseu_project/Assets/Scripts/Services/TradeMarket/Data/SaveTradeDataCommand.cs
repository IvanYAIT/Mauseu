using System.Collections.Generic;
using System.IO;
using FrostLib.Commands;
using Newtonsoft.Json;
using Services.Inventory.Items;
using UnityEngine;

namespace Services.TradeMarket.Data
{
    public class SaveTradeDataCommand : ICommand
    {
        private const string ContainerName = "Inventory.json";

        private readonly Dictionary<ItemType, int> _data;
        
        public SaveTradeDataCommand(Dictionary<ItemType, int> data) => _data = data;

        public void Execute()
        {
            var path = Path.Join(Application.persistentDataPath, ContainerName);
            var data = JsonConvert.SerializeObject(_data);
            File.WriteAllText(path, data);
        }
    }
}