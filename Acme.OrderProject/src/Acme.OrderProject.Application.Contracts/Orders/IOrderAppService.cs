using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.OrderProject.Orders
{
    public interface IOrderAppService : IApplicationService
    {
        Task<OrderDto> GetAsync(Guid id);
        Task<PagedResultDto<OrderDto>> GetListAsync(PagedAndSortedResultRequestDto input);
        Task<OrderDto> CreateAsync(CreateOrderDto input);
        Task<OrderDto> AddLineAsync(AddOrderLineDto input);
        Task ApproveAsync(Guid orderId);
        Task DeleteAsync(Guid id);
    }
}
