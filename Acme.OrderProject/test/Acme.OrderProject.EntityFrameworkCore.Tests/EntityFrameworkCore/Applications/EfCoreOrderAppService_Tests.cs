using Acme.OrderProject.Orders;
using Xunit;

namespace Acme.OrderProject.EntityFrameworkCore.Applications;

[Collection(OrderProjectTestConsts.CollectionDefinitionName)]
public class EfCoreOrderAppService_Tests : OrderAppService_Tests<OrderProjectEntityFrameworkCoreTestModule>
{
}
