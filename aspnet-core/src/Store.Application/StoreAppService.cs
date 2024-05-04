using System;
using System.Collections.Generic;
using System.Text;
using Store.Localization;
using Volo.Abp.Application.Services;

namespace Store;

/* Inherit your application services from this class.
 */
public abstract class StoreAppService : ApplicationService
{
    protected StoreAppService()
    {
        LocalizationResource = typeof(StoreResource);
    }
}
