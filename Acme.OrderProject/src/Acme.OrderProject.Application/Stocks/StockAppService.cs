using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Application.Dtos;

namespace Acme.OrderProject.Stocks
{
    public class StockAppService : ApplicationService, IStockAppService
    {
        private readonly IStockRepository _stockRepository;
        private readonly StockManager _stockManager;

        public StockAppService(
            IStockRepository stockRepository,
            StockManager stockManager)
        {
            _stockRepository = stockRepository;
            _stockManager = stockManager;
        }

        public async Task<StockDto> GetAsync(Guid id)
        {
            var stock = await _stockRepository.GetAsync(id);
            return ObjectMapper.Map<Stock, StockDto>(stock);
        }
        public async Task<PagedResultDto<StockDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var stocks = await _stockRepository.GetPagedListAsync(input.SkipCount, input.MaxResultCount, input.Sorting ?? nameof(Stock.Name));
            var totalCount = await _stockRepository.GetCountAsync();
            return new PagedResultDto<StockDto>(totalCount, ObjectMapper.Map<List<Stock>, List<StockDto>>(stocks));
        }
        public async Task<StockDto> CreateAsync(CreateUpdateStockDto input)
        {
            var stock = await _stockManager.CreateAsync(
                input.Name,
                input.Quantity,
                input.Price);

            stock = await _stockRepository.InsertAsync(stock, autoSave: true);
            return ObjectMapper.Map<Stock, StockDto>(stock);
        }
        public async Task<StockDto> UpdateAsync(Guid id, CreateUpdateStockDto input)
        {
            var stock = await _stockRepository.GetAsync(id);
            stock.Name = input.Name;
            stock.Quantity = input.Quantity;
            stock.Price = input.Price;
            await _stockRepository.UpdateAsync(stock, autoSave: true);
            return ObjectMapper.Map<Stock, StockDto>(stock);
        }
        public async Task DeleteAsync(Guid id)
        {
            await _stockManager.CheckDeleteAsync(id);
            await _stockRepository.DeleteAsync(id, autoSave: true);

        }




    }
}