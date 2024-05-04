using Volo.Abp.Settings;

namespace Store.Settings;

public class StoreSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(StoreSettings.MySetting1));
    }
}
