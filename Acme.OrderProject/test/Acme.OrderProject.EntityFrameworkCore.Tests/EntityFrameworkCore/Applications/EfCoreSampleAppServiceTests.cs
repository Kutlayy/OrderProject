using Acme.OrderProject.Samples;
using Xunit;

namespace Acme.OrderProject.EntityFrameworkCore.Applications;

[Collection(OrderProjectTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<OrderProjectEntityFrameworkCoreTestModule>
{

}
