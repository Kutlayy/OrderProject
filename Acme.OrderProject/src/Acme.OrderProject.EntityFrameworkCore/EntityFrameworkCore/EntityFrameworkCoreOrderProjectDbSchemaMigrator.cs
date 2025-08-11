using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Acme.OrderProject.Data;
using Volo.Abp.DependencyInjection;

namespace Acme.OrderProject.EntityFrameworkCore;

public class EntityFrameworkCoreOrderProjectDbSchemaMigrator
    : IOrderProjectDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreOrderProjectDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the OrderProjectDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<OrderProjectDbContext>()
            .Database
            .MigrateAsync();
    }
}
