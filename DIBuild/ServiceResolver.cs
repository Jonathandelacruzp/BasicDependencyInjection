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
            var type = _container.GetDependencyType(typeof(T));
            return (T) GetService(type);
        }

        private object GetService(Type type)
        {
            if (_objectDictionary.TryGetValue(type.Name, out var instance))
                return instance;

            try
            {
                var constructor = type.GetConstructors().Single();
                var constructorParameters = constructor.GetParameters();
                if (constructorParameters.Length == 0)
                {
                    instance = Activator.CreateInstance(type);
                    return instance;
                }

                var objectParameters = constructorParameters.Select(parameter => GetService(parameter.ParameterType)).ToArray();
                instance = Activator.CreateInstance(type, objectParameters);
                return instance;
            }
            catch (Exception ex)
            {
                throw new Exception($"Could not create and instance for type '{type.Name}'", ex);
            }
            finally
            {
                if (instance != null)
                    _objectDictionary.Add(type.Name, instance);
            }
        }
    }
}