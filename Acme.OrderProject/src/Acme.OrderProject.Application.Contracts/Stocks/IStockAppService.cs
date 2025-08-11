using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;

namespace Acme.OrderProject.Stocks
{
    public interface IStockAppService : IApplicationService
    {
        Task<StockDto> GetAsync(Guid id);
        Task<PagedResultDto<StockDto>> GetListAsync(PagedAndSortedResultRequestDto input);
        Task<StockDto> CreateAsync(CreateUpdateStockDto input);
        Task<StockDto> UpdateAsync(Guid id, CreateUpdateStockDto input);
        Task DeleteAsync(Guid id);


    }
}
