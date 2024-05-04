using System;
using System.Collections.Generic;
using System.Text;
using Store.Localization;
using Volo.Abp.Application.Services;

namespace Store.Admin;

/* Inherit your application services from this class.
 */
public abstract class StoreAdminAppService : ApplicationService
{
    protected StoreAdminAppService()
    {
        LocalizationResource = typeof(StoreResource);
    }
}
