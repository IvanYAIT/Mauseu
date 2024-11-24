using System.IO;
using FrostLib.Commands;
using Newtonsoft.Json;
using UnityEngine;

namespace Shared.DataProvider.Commands
{
    public abstract class SaveDataCommand<T> : ICommand
    {
        private readonly T _data;
        
        public SaveDataCommand(T data) => _data = data;

        public void Execute()
        {
            var containerName = GetContainerName();
            var path = Path.Join(Application.persistentDataPath, containerName);
            var data = JsonConvert.SerializeObject(_data);
            File.WriteAllText(path, data);
        }
        
        protected abstract string GetContainerName();
    }
}