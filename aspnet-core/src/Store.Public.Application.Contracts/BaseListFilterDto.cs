using System.ComponentModel.DataAnnotations;

namespace Store.Public
{
    public class BaseListFilterDto : PagedResultRequestBase
    {
        public string Keyword { get; set; }
    }
}
