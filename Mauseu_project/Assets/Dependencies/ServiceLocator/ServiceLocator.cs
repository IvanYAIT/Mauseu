using System;
using System.Collections.Generic;

namespace Dependencies.ChaserLib.ServiceLocator
{
    public class ServiceLocator
    {
        public static ServiceLocator Instance => _instance ??= new ServiceLocator();
        private static ServiceLocator _instance;

        private readonly Dictionary<Type, object> _services = new();

        public void Add<TService>(TService service)
        {
            var serviceType = typeof(TService);

            if (_services.ContainsKey(serviceType))
            {
                _services[serviceType] = service;
                return;
            }

            _services.Add(typeof(TService), service);
        }

        public TService Get<TService>() => (TService) _services[typeof(TService)];
    }
}