using Acme.OrderProject.Samples;
using Xunit;

namespace Acme.OrderProject.EntityFrameworkCore.Domains;

[Collection(OrderProjectTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<OrderProjectEntityFrameworkCoreTestModule>
{

}
