using System;
using System.Collections.Generic;
using System.Linq;

namespace DIBuild
{
    public class ServiceContainer
    {
        private readonly ICollection<(Type, ServiceLifeTime)> _dependencies;

        public ServiceContainer()
        {
            _dependencies = new List<(Type, ServiceLifeTime)>();
        }

        public void AddSingleton<T>()
        {
            _dependencies.Add((typeof(T), ServiceLifeTime.Singleton));
        }

        public void AddSingleton(Type type)
        {
            _dependencies.Add((type, ServiceLifeTime.Singleton));
        }

        public void AddTransient<T>()
        {
            _dependencies.Add((typeof(T), ServiceLifeTime.Transient));
        }

        public void AddTransient(Type type)
        {
            _dependencies.Add((type, ServiceLifeTime.Transient));
        }

        public (Type, ServiceLifeTime) GetServiceType(Type type)
        {
            return _dependencies.First(x => x.Item1.Name == type.Name);
        }

        public ServiceLifeTime GetServiceLifetime(Type type)
        {
            return _dependencies.First(x => x.Item1.Name == type.Name).Item2;
        }
    }

    public enum ServiceLifeTime
    {
        Singleton,
        Transient
    }
}