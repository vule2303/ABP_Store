using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;

namespace Store.Admin.Controllers;

public class AdminHomeController : AbpController
{
    public ActionResult Index()
    {
        return Redirect("~/swagger");
    }
}
