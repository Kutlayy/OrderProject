using Acme.OrderProject.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.OrderProject.Stocks
{
    public class EfCoreStockRepository : EfCoreRepository<OrderProjectDbContext, Stock, Guid>, IStockRepository
    {
        public EfCoreStockRepository(IDbContextProvider<OrderProjectDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public Task<bool> IsUsedInOrdersAsync(Guid stockId)
        {
            throw new NotImplementedException();
        }
    }
}
