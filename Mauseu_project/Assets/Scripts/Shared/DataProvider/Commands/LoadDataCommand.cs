using System.IO;
using FrostLib.Commands;
using Newtonsoft.Json;
using UnityEngine;

namespace Shared.DataProvider.Commands
{
    public abstract class LoadDataCommand<T> : ICommand<T>
    {
        public T Execute()
        {
            var containerName = GetContainerName();
            var path = Path.Join(Application.persistentDataPath, containerName);

            if (!File.Exists(path))
                return GetDefault();

            var data = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(data);
        }
        
        protected abstract T GetDefault();
        
        protected abstract string GetContainerName();
    }
}