using System;

namespace DIBuild
{
    public class Dependency : IDisposable
    {
        public Type ImplementorType { get; }
        public Type Type { get; }
        public ServiceLifeTime ServiceLifeTime { get; }
        public object Instance { get; private set; }
        public bool HasInstance { get; private set; }
        public bool IsInterfaceType { get; }

        private Dependency(Type type, Type implementorType, ServiceLifeTime serviceLifeTime)
        {
            IsInterfaceType = type.IsInterface;
            if (IsInterfaceType)
                ImplementorType = implementorType ?? throw new Exception("Dependency implementor type with Interface base Type can not be null");

            Type = type;
            ServiceLifeTime = serviceLifeTime;
        }

        public static class Factory
        {
            public static Dependency Create(Type type, Type implementorType = null, ServiceLifeTime serviceLifeTime = ServiceLifeTime.Transient)
            {
                return new(type, implementorType, serviceLifeTime);
            }

            public static Dependency Create(Dependency dependency)
            {
                return new(dependency.Type, dependency.ImplementorType, dependency.ServiceLifeTime);
            }
        }

        public void SetInstance(object instance)
        {
            if (ServiceLifeTime == ServiceLifeTime.Transient)
                return;

            Instance = instance;
            HasInstance = true;
        }

        #region Dispose

        private void ReleaseUnmanagedResources()
        {
            if (Instance is IDisposable disposable)
                disposable.Dispose();

            Instance = default;
            HasInstance = false;
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~Dependency()
        {
            ReleaseUnmanagedResources();
        }

        #endregion
    }

    public enum ServiceLifeTime
    {
        Singleton = 0,
        Scoped = 1,
        Transient = 2
    }
}