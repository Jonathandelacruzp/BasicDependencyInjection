using System;
using System.Linq;

namespace DIBuild
{
    public class DependencyResolver
    {
        private readonly DependencyContainer _container;

        public DependencyResolver(DependencyContainer container)
        {
            _container = container;
        }

        public T GetService<T>()
        {
            var dependency = _container.GetServiceType(typeof(T));
            return (T) GetService(dependency);
        }

        private object GetService(Dependency dependency)
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
                        var dependencyParam = _container.GetServiceType(parameter.ParameterType);
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
                if (dependency.ServiceLifeTime == ServiceLifeTime.Singleton && instance != null)
                    dependency.SetInstance(instance);
            }
        }
    }
}