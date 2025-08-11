using Shouldly;
using System;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Xunit;

namespace Acme.OrderProject.Customers;

public abstract class CustomerAppService_Tests<TStartupModule> : OrderProjectApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly ICustomerAppService _customerAppService;

    protected CustomerAppService_Tests()
    {
        _customerAppService = GetRequiredService<ICustomerAppService>();
    }

    [Fact]
    public async Task Should_Create_A_New_Customer()
    {
        var input = new CreateUpdateCustomerDto
        {
            Name = "New Customer",
            RiskLimit = 5000m,
            BillAddress = "Address"
        };

        var result = await _customerAppService.CreateAsync(input);

        result.Id.ShouldNotBe(Guid.Empty);
        result.Name.ShouldBe("New Customer");
        result.RiskLimit.ShouldBe(5000m);
    }
}