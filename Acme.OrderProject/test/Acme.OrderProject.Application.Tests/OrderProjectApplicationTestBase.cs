using Volo.Abp.Modularity;

namespace Acme.OrderProject;

public abstract class OrderProjectApplicationTestBase<TStartupModule> : OrderProjectTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
