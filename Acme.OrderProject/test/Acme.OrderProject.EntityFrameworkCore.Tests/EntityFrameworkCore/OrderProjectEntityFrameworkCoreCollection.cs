using Xunit;

namespace Acme.OrderProject.EntityFrameworkCore;

[CollectionDefinition(OrderProjectTestConsts.CollectionDefinitionName)]
public class OrderProjectEntityFrameworkCoreCollection : ICollectionFixture<OrderProjectEntityFrameworkCoreFixture>
{

}
