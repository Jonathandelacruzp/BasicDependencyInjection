﻿using System;
using System.Linq;
using System.Collections.Generic;

namespace DIBuild
{
    public class ScopeDependencyResolver : DependencyResolver, IDisposable
    {
        private ICollection<Dependency> ScopeDependencies { get; }

        public ScopeDependencyResolver(DependencyContainer dependencyContainer) : base(dependencyContainer)
        {
            ScopeDependencies = dependencyContainer.GetScopeDependencies();
        }

        public virtual T GetService<T>()
        {
            return (T) GetService(GetDependency(typeof(T)));
        }

        private Dependency GetDependency(Type type)
        {
            var dependency = ScopeDependencies.FirstOrDefault(x => x.Type.Name == type.Name);
            return dependency ?? DependencyContainer.GetDependencyByType(type);
        }

        protected override object GetService(Dependency dependency)
        {
            if (dependency.HasInstance)
                return dependency.Instance;

            object instance = null;

            try
            {
                var constructor = dependency.Type.GetConstructors().Single();
                var constructorParameters = constructor.GetParameters();
                if (constructorParameters.Length == 0)
                    return instance = Activator.CreateInstance(dependency.Type);

                var objectParameters = constructorParameters
                    .Select(parameter =>
                    {
                        var dependencyParam = GetDependency(parameter.ParameterType);
                        return GetService(dependencyParam);
                    })
                    .ToArray();

                return instance = Activator.CreateInstance(dependency.Type, objectParameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not create and instance for type '{dependency.Type.Name}'", ex);
            }
            finally
            {
                if (dependency.ServiceLifeTime != ServiceLifeTime.Transient && instance != null)
                    dependency.SetInstance(instance);
            }
        }

        #region Dispose

        private void ReleaseUnmanagedResources()
        {
            foreach (var dependency in ScopeDependencies)
                dependency.Dispose();

            ScopeDependencies.Clear();
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~ScopeDependencyResolver()
        {
            ReleaseUnmanagedResources();
        }

        #endregion
    }
}