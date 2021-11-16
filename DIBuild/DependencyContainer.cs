namespace DIBuild;

public partial class DependencyContainer
{
    private readonly ICollection<Dependency> _dependencies;

    public DependencyContainer()
    {
        _dependencies = new List<Dependency>();
    }

    public Dependency GetDependencyByType(Type type)
    {
        return _dependencies.First(x => x.Type.Name == type.Name);
    }

    public ICollection<Dependency> GetScopeDependencies()
    {
        return _dependencies.Where(x => x.ServiceLifeTime == ServiceLifeTime.Scoped)
            .Select(Dependency.Factory.Create)
            .ToList();
    }
}