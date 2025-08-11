using Acme.OrderProject.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Acme.OrderProject.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class OrderProjectController : AbpControllerBase
{
    protected OrderProjectController()
    {
        LocalizationResource = typeof(OrderProjectResource);
    }
}
