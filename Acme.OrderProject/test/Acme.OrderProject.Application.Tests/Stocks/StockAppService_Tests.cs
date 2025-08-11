using Shouldly;
using System;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Xunit;

namespace Acme.OrderProject.Stocks;

public abstract class StockAppService_Tests<TStartupModule> : OrderProjectApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IStockAppService _stockAppService;

    protected StockAppService_Tests()
    {
        _stockAppService = GetRequiredService<IStockAppService>();
    }

    [Fact]
    public async Task Should_Create_Stock()
    {
        var input = new CreateUpdateStockDto
        {
            Name = "Test Stock",
            Quantity = 10,
            Price = 5m
        };

        var result = await _stockAppService.CreateAsync(input);

        result.Id.ShouldNotBe(Guid.Empty);
        result.Name.ShouldBe("Test Stock");
        result.Quantity.ShouldBe(10);
        result.Price.ShouldBe(5m);
    }
}