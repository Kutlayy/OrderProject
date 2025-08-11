using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories; 
namespace Acme.OrderProject.Stocks
{
    public interface IStockRepository : IRepository<Stock, Guid>
    {
        Task<bool> IsUsedInOrdersAsync(Guid stockId);
    }
}
