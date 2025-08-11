using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Acme.OrderProject.EntityFrameworkCore;

namespace Acme.OrderProject.Customers
{
    public class EfCoreCustomerRepository : EfCoreRepository<OrderProjectDbContext, Customer, Guid>, ICustomerRepository
    {
        public EfCoreCustomerRepository(
            IDbContextProvider<OrderProjectDbContext> dbContextProvider
        ) : base(dbContextProvider)
        {
        }

        public async Task<bool> HasOrdersAsync(Guid customerId)
        {
            var dbContext = await GetDbContextAsync();
            return await dbContext.Orders.AnyAsync(o => o.CustomerId == customerId);
        }
    }
}