namespace DIBuild;

public class ScopeDependencyResolver : DependencyResolver, IDisposable
{
    private ICollection<Dependency> ScopeDependencies { get; }

    public ScopeDependencyResolver(DependencyContainer dependencyContainer) : base(dependencyContainer)
    {
        ScopeDependencies = dependencyContainer.GetScopeDependencies();
    }

    public override T GetService<T>()
    {
        return (T)GetService(GetDependency(typeof(T)));
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

        if (dependency.IsFuncInstance)
        {
            dependency.SetFuncInstance();
            return dependency.Instance;
        }

        var implementorType = dependency.IsInterfaceType ? dependency.ImplementorType : dependency.Type;
        object instance = null;

        try
        {
            var constructor = implementorType.GetConstructors().Single();
            var constructorParameters = constructor.GetParameters();
            if (constructorParameters.Length == 0)
                return instance = Activator.CreateInstance(implementorType);

            var objectParameters = constructorParameters
                .Select(parameter =>
                {
                    var dependencyParam = GetDependency(parameter.ParameterType);
                    if (dependencyParam.ServiceLifeTime > dependency.ServiceLifeTime)
                        throw new Exception($"Dependency {implementorType.Name} should contain {dependency.ServiceLifeTime.ToString()} parameters on constructor");

                    return GetService(dependencyParam);
                })
                .ToArray();

            return instance = Activator.CreateInstance(implementorType, objectParameters);
        }
        catch (Exception ex)
        {
            throw new Exception($"Could not create and instance for type '{implementorType.Name}'", ex);
        }
        finally
        {
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