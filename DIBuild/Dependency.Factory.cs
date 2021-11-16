namespace DIBuild;

public partial class Dependency
{
    public static class Factory
    {
        public static Dependency Create(Type type, Type implementorType = null, ServiceLifeTime serviceLifeTime = ServiceLifeTime.Transient)
        {
            return new(type, implementorType, serviceLifeTime);
        }

        public static Dependency Create(Type type, ServiceLifeTime serviceLifeTime, Func<object> func = null)
        {
            var dependency = new Dependency(type, null, serviceLifeTime);
            dependency.SetFunc(func);
            return dependency;
        }

        public static Dependency Create(Dependency dependency)
        {
            return new(dependency.Type, dependency.ImplementorType, dependency.ServiceLifeTime);
        }
    }
}