using Store.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Store.Admin.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class StoreAdminController : AbpControllerBase
{
    protected StoreAdminController()
    {
        LocalizationResource = typeof(StoreResource);
    }
}
