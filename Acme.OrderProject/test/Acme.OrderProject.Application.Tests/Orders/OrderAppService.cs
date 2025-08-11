using Acme.OrderProject.Customers;
using Shouldly;
using System;
using System.Threading.Tasks;
using Volo.Abp.Modularity;
using Xunit;

namespace Acme.OrderProject.Orders;

public abstract class OrderAppService_Tests<TStartupModule> : OrderProjectApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    private readonly IOrderAppService _orderAppService;
    private readonly ICustomerAppService _customerAppService;

    protected OrderAppService_Tests()
    {
        _orderAppService = GetRequiredService<IOrderAppService>();
        _customerAppService = GetRequiredService<ICustomerAppService>();
    }

    [Fact]
    public async Task Should_Create_Order()
    {
        var customer = await _customerAppService.CreateAsync(new CreateUpdateCustomerDto
        {
            Name = "Order Customer",
            RiskLimit = 1000m,
            BillAddress = "Address"
        });

        var order = await _orderAppService.CreateAsync(new CreateOrderDto
        {
            CustomerId = customer.Id,
            OrderDate = DateTime.Today,
            DeliveryAddress = "Address"
        });

        order.Id.ShouldNotBe(Guid.Empty);
        order.CustomerId.ShouldBe(customer.Id);
        order.IsApproved.ShouldBeFalse();
        order.TotalAmount.ShouldBe(0m);
    }
}