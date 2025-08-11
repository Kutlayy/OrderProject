using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Acme.OrderProject.Data;

/* This is used if database provider does't define
 * IOrderProjectDbSchemaMigrator implementation.
 */
public class NullOrderProjectDbSchemaMigrator : IOrderProjectDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
