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
            _dependencies.Add(Dependency.Factory.Create(typeof(T)));
        }

        public Dependency GetDependencyByType(Type type)
        {
            return _dependencies.First(x => x.Type.Name == type.Name);
        }

        public ICollection<Dependency> GetScopeDependencies()
        {
            return _dependencies.Where(x => x.ServiceLifeTime == ServiceLifeTime.Scoped)
                .Select(Dependency.Factory.Create)
                .ToList();
        }

        public void AddScoped<T>()
        {
            _dependencies.Add(Dependency.Factory.Create(typeof(T), ServiceLifeTime.Scoped));
        }
    }
}