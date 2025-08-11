using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.OrderProject.Customers
{
    public interface ICustomerAppService : IApplicationService
    {
        Task<CustomerDto> GetAsync(Guid id);
        Task<PagedResultDto<CustomerDto>> GetListAsync(PagedAndSortedResultRequestDto input);
        Task<CustomerDto> CreateAsync(CreateUpdateCustomerDto input);
        Task<CustomerDto> UpdateAsync(Guid id, CreateUpdateCustomerDto input);
        Task DeleteAsync(Guid id);
    }
}
