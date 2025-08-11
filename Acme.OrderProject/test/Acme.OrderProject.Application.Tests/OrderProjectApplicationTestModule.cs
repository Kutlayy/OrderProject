using Volo.Abp.Modularity;

namespace Acme.OrderProject;

[DependsOn(
    typeof(OrderProjectApplicationModule),
    typeof(OrderProjectDomainTestModule)
)]
public class OrderProjectApplicationTestModule : AbpModule
{

}
