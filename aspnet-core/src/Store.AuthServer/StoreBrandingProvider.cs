using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Store;

[Dependency(ReplaceServices = true)]
public class StoreBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Store";
}
