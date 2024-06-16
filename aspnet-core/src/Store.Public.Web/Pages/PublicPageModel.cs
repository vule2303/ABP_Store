using Store.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Store.Public.Web.Pages;

/* Inherit your PageModel classes from this class.
 */
public abstract class PublicPageModel : AbpPageModel
{
    protected PublicPageModel()
    {
        LocalizationResourceType = typeof(StoreResource);
    }
}
