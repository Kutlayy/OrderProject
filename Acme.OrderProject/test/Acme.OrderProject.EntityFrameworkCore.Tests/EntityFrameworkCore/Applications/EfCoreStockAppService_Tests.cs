using Acme.OrderProject.Stocks;
using Xunit;

namespace Acme.OrderProject.EntityFrameworkCore.Applications;

[Collection(OrderProjectTestConsts.CollectionDefinitionName)]
public class EfCoreStockAppService_Tests : StockAppService_Tests<OrderProjectEntityFrameworkCoreTestModule>
{
}