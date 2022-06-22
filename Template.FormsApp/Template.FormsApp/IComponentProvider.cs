namespace Template.FormsApp;

using Smart.Resolver;

public interface IComponentProvider
{
    void RegisterComponents(ResolverConfig config);
}
