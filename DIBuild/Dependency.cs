using System;

namespace DIBuild
{
    public class Dependency
    {
        public Type Type { get; private set; }
        public ServiceLifeTime ServiceLifeTime { get; private set; }
        public object Instance { get; private set; }
        public bool HasInstance { get; private set; }

        public static class Factory
        {
            public static Dependency Create(Type type, ServiceLifeTime serviceLifeTime)
            {
                return new() {Type = type, ServiceLifeTime = serviceLifeTime};
            }
        }

        public void SetInstance(object instance)
        {
            Instance = instance;
            HasInstance = true;
        }
    }

    public enum ServiceLifeTime
    {
        Singleton,
        Transient
    }
}