using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;
using Volo.Abp.Guids;

namespace Acme.OrderProject.Stocks;

public class StockManager : DomainService
{
    private readonly IStockRepository _stockRepository;
    private readonly IGuidGenerator _guidGenerator;

    public StockManager(
        IStockRepository stockRepository,
        IGuidGenerator guidGenerator)
    {
        _stockRepository = stockRepository;
        _guidGenerator = guidGenerator;
    }

    public Task<Stock> CreateAsync(string name, int quantity, decimal price)
    {
        var stock = new Stock(_guidGenerator.Create(), name, quantity, price);
        return Task.FromResult(stock);
    }

    public async Task CheckDeleteAsync(Guid id)
    {
        if (await _stockRepository.IsUsedInOrdersAsync(id))
        {
            throw new BusinessException("Cannot delete stock with existing orders.");
        }
    }
}