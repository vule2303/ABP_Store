using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.Account.Web.Pages.Account;
using Volo.Abp.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace Store.Pages.Account.Model
{
    public class CustomRegisterModel : RegisterModel
    {
        public CustomRegisterModel(
            IAccountAppService accountAppService)
            : base(accountAppService)
        {
            // Không cần thêm logic xử lý khác vào constructor nếu không cần thiết
        }
        
    }
}