using System.Threading.Tasks;

namespace Acme.OrderProject.Data;

public interface IOrderProjectDbSchemaMigrator
{
    Task MigrateAsync();
}
