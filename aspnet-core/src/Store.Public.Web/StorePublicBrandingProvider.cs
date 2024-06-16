using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Store.Public.Web;

[Dependency(ReplaceServices = true)]
public class StorePublicBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Public";
}
