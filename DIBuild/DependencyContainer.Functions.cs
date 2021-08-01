using System;
using System.Collections.Generic;
using System.Linq;

namespace DIBuild
{
    public partial class DependencyContainer
    {
        public void AddSingleton(Type type)
        {
            _dependencies.Add(Dependency.Factory.Create(type, null, ServiceLifeTime.Singleton));
        }

        public void AddSingleton<T>() where T : class
        {
            _dependencies.Add(Dependency.Factory.Create(typeof(T), null, ServiceLifeTime.Singleton));
        }

        public void AddSingleton<TInterface, TImplementor>() where TImplementor : TInterface
        {
            _dependencies.Add(Dependency.Factory.Create(typeof(TInterface), typeof(TImplementor), ServiceLifeTime.Singleton));
        }

        public void AddSingleton<T>(Func<T> func)
        {
            _dependencies.Add(Dependency.Factory.Create(typeof(T), ServiceLifeTime.Singleton, () => func()));
        }

        public void AddScoped(Type type)
        {
            _dependencies.Add(Dependency.Factory.Create(type, null, ServiceLifeTime.Scoped));
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

        public void AddTransient(Type type)
        {
            _dependencies.Add(Dependency.Factory.Create(type));
        }

        public void AddTransient<T>() where T : class
        {
            _dependencies.Add(Dependency.Factory.Create(typeof(T)));
        }

        public void AddTransient<TInterface, TImplementor>() where TImplementor : TInterface
        {
            _dependencies.Add(Dependency.Factory.Create(typeof(TInterface), typeof(TImplementor)));
        }

        public void AddTransient<T>(Func<T> func)
        {
            _dependencies.Add(Dependency.Factory.Create(typeof(T), ServiceLifeTime.Transient, () => func()));
        }
    }
}