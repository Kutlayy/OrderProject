using Acme.OrderProject.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Acme.OrderProject.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(OrderProjectEntityFrameworkCoreModule),
    typeof(OrderProjectApplicationContractsModule)
)]
public class OrderProjectDbMigratorModule : AbpModule
{
}
