using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Acme.OrderProject.EntityFrameworkCore;

namespace Acme.OrderProject.Orders
{
    public class EfCoreOrderRepository
        : EfCoreRepository<OrderProjectDbContext, Order, Guid>, IOrderRepository
    {
        public EfCoreOrderRepository(
            IDbContextProvider<OrderProjectDbContext> dbContextProvider
        ) : base(dbContextProvider)
        {
        }

        public async Task<Order> GetWithLinesAsync(Guid id)
        {
            var dbContext = await GetDbContextAsync();
            return await dbContext.Orders
                .Include(o => o.Lines)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
