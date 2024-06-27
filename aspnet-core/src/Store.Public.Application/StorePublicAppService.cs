using Store.Localization;
using Volo.Abp.Application.Services;

namespace Store.Public;

/* Inherit your application services from this class.
 */
public abstract class StorePublicAppService : ApplicationService
{
    protected StorePublicAppService()
    {
        LocalizationResource = typeof(StoreResource);
    }
}
