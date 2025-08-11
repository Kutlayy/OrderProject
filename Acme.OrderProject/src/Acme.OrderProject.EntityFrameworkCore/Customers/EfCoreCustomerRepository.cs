using Acme.OrderProject.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.OrderProject.Customers
{
    public class EfCoreCustomerRepository : EfCoreRepository<OrderProjectDbContext, Customer, Guid>, ICustomerRepository        
    {
        private static IDbContextProvider<OrderProjectDbContext> dbContextProvider;

        public EfCoreCustomerRepository(OrderProjectDbContext dbContext) : base(dbContextProvider)
        {
        }

        public Task<bool> HasOrdersAsync(Guid customerId)
        {
            throw new NotImplementedException();
        }
    }
}
