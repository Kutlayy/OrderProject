using Acme.OrderProject.Customers;
using Xunit;

namespace Acme.OrderProject.EntityFrameworkCore.Applications;

[Collection(OrderProjectTestConsts.CollectionDefinitionName)]
public class EfCoreCustomerAppService_Tests : CustomerAppService_Tests<OrderProjectEntityFrameworkCoreTestModule>
{
}
