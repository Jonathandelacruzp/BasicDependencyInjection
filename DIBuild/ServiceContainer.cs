using System;
using System.Collections.Generic;
using System.Linq;

namespace DIBuild
{
    public class ServiceContainer
    {
        private readonly IList<Type> _dependencies;

        public ServiceContainer()
        {
            _dependencies = new List<Type>();
        }

        public void AddService<T>()
        {
            _dependencies.Add(typeof(T));
        }
        
        public void AddService(Type type)
        {
            _dependencies.Add(type);
        }

        public Type GetDependencyType(Type type)
        {
            return _dependencies.First(x => x.Name == type.Name);
        }
    }
}