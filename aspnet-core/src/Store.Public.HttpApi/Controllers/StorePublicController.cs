using Store.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Store.Public.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class StorePublicController : AbpControllerBase
{
    protected StorePublicController()
    {
        LocalizationResource = typeof(StoreResource);
    }
}
