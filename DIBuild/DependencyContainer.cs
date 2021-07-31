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

        public void AddSingleton<TInterface, TImplementor>() where TImplementor : TInterface
        {
            _dependencies.Add(Dependency.Factory.Create(typeof(TInterface), typeof(TImplementor), ServiceLifeTime.Singleton));
        }

        public void AddSingleton<T>() where T : class
        {
            _dependencies.Add(Dependency.Factory.Create(typeof(T), null, ServiceLifeTime.Singleton));
        }

        public void AddSingleton<T>(Func<T> func)
        {
            _dependencies.Add(Dependency.Factory.Create(typeof(T), ServiceLifeTime.Singleton, () => func()));
        }

        public void AddSingleton(Type type)
        {
            _dependencies.Add(Dependency.Factory.Create(type, null, ServiceLifeTime.Singleton));
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

        public void AddScoped<T>() where T : class
        {
            _dependencies.Add(Dependency.Factory.Create(typeof(T), null, ServiceLifeTime.Scoped));
        }

        public void AddScoped<TInterface, TImplementor>() where TImplementor : TInterface
        {
            _dependencies.Add(Dependency.Factory.Create(typeof(TInterface), typeof(TImplementor), ServiceLifeTime.Scoped));
        }

        public void AddScoped<T>(Func<T> func)
        {
            _dependencies.Add(Dependency.Factory.Create(typeof(T), ServiceLifeTime.Scoped, () => func()));
        }
    }
}