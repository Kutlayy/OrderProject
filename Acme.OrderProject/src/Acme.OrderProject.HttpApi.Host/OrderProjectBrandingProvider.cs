using Microsoft.Extensions.Localization;
using Acme.OrderProject.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Acme.OrderProject;

[Dependency(ReplaceServices = true)]
public class OrderProjectBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<OrderProjectResource> _localizer;

    public OrderProjectBrandingProvider(IStringLocalizer<OrderProjectResource> localizer)
    {
        _localizer = localizer;
    }

  
}
