using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DIBuild
{
    public class ServiceResolver
    {
        private readonly ServiceContainer _container;
        private readonly IDictionary<string, object> _objectDictionary;

        public ServiceResolver(ServiceContainer container)
        {
            _container = container;
            _objectDictionary = new ConcurrentDictionary<string, object>();
        }

        public T GetService<T>()
        {
            var (type, serviceLifeTime) = _container.GetServiceType(typeof(T));
            return (T) GetService(type, serviceLifeTime);
        }

        private object GetService(Type type, ServiceLifeTime serviceLifeTime)
        {
            if (_objectDictionary.TryGetValue(type.Name, out var instance)
                && serviceLifeTime == ServiceLifeTime.Singleton)
                return instance;

            try
            {
                var constructor = type.GetConstructors().Single();
                var constructorParameters = constructor.GetParameters();
                if (constructorParameters.Length == 0)
                    return instance = Activator.CreateInstance(type);

                var objectParameters = constructorParameters
                    .Select(parameter => GetService(parameter.ParameterType, _container.GetServiceLifetime(parameter.ParameterType)))
                    .ToArray();

                return instance = Activator.CreateInstance(type, objectParameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not create and instance for type '{type.Name}'", ex);
            }
            finally
            {
                if (serviceLifeTime == ServiceLifeTime.Singleton)
                    _objectDictionary.Add(type.Name, instance);
            }
        }
    }
}