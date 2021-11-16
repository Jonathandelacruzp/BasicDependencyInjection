namespace DIBuild;

public class DependencyResolver
{
    protected readonly DependencyContainer DependencyContainer;

    public DependencyResolver(DependencyContainer container)
    {
        DependencyContainer = container;
    }

    public virtual T GetService<T>()
    {
        var dependency = DependencyContainer.GetDependencyByType(typeof(T));
        return (T)GetService(dependency);
    }

    protected virtual object GetService(Dependency dependency)
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
                    var dependencyParam = DependencyContainer.GetDependencyByType(parameter.ParameterType);
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