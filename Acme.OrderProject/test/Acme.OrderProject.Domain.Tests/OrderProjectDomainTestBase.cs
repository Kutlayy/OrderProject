using Volo.Abp.Modularity;

namespace Acme.OrderProject;

/* Inherit from this class for your domain layer tests. */
public abstract class OrderProjectDomainTestBase<TStartupModule> : OrderProjectTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
