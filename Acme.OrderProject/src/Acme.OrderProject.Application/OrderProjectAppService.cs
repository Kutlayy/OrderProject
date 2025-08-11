using Acme.OrderProject.Localization;
using Volo.Abp.Application.Services;

namespace Acme.OrderProject;

/* Inherit your application services from this class.
 */
public abstract class OrderProjectAppService : ApplicationService
{
    protected OrderProjectAppService()
    {
        LocalizationResource = typeof(OrderProjectResource);
    }
}
