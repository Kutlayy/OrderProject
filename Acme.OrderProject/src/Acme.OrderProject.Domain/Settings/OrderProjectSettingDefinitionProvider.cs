using Volo.Abp.Settings;

namespace Acme.OrderProject.Settings;

public class OrderProjectSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(OrderProjectSettings.MySetting1));
    }
}
