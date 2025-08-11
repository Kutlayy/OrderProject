using Volo.Abp.Modularity;

namespace Acme.OrderProject;

[DependsOn(
    typeof(OrderProjectDomainModule),
    typeof(OrderProjectTestBaseModule)
)]
public class OrderProjectDomainTestModule : AbpModule
{

}
