using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.OrderProject.Orders
{
    public interface IOrderRepository : IRepository<Order, Guid>
    {
        Task<Order> GetWithLinesAsync(Guid id);
    }
}
