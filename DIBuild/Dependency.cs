namespace DIBuild;

public sealed partial class Dependency : IDisposable
{
    public Type ImplementorType { get; }
    public Type Type { get; }
    public ServiceLifeTime ServiceLifeTime { get; }
    public object Instance { get; private set; }
    public bool HasInstance { get; private set; }
    public bool IsInterfaceType { get; }
    public bool IsFuncInstance { get; private set; }
    private Func<object> Func { get; set; }

    private Dependency(Type type, Type implementorType, ServiceLifeTime serviceLifeTime)
    {
        if (implementorType?.GetNestedTypes().Contains(type) == false)
            throw new InvalidOperationException(nameof(implementorType) + " does not implements " + nameof(type));

        IsInterfaceType = type.IsInterface;
        ImplementorType = implementorType;
        Type = type;
        ServiceLifeTime = serviceLifeTime;
    }
}

public enum ServiceLifeTime
{
    Singleton = 0,
    Scoped = 1,
    Transient = 2
}