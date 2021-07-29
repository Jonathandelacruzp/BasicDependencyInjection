using System;
using System.Collections.Generic;
using System.Linq;

namespace DIBuild
{
    public class DependencyContainer
    {
        private readonly ICollection<Dependency> _dependencies;

        public DependencyContainer()
        {
            _dependencies = new List<Dependency>();
        }

        public void AddSingleton<T>()
        {
            _dependencies.Add(Dependency.Factory.Create(typeof(T), ServiceLifeTime.Singleton));
        }

        public void AddSingleton(Type type)
        {
            _dependencies.Add(Dependency.Factory.Create(type, ServiceLifeTime.Singleton));
        }

        public void AddTransient<T>()
        {
            _dependencies.Add(Dependency.Factory.Create(typeof(T), ServiceLifeTime.Transient));
        }

        public void AddTransient(Type type)
        {
            _dependencies.Add(Dependency.Factory.Create(type, ServiceLifeTime.Transient));
        }

        public Dependency GetServiceType(Type type)
        {
            return _dependencies.First(x => x.Type.Name == type.Name);
        }
    }
}