using Acme.OrderProject.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> IsUsedInOrdersAsync(Guid stockId)
        {
            var dbContext = await GetDbContextAsync();
            return await dbContext.OrderLines.AnyAsync(l => l.StockId == stockId);
        }
    }
}