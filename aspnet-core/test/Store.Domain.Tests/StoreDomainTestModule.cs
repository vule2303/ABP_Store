using Store.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Store;

[DependsOn(
    typeof(StoreEntityFrameworkCoreTestModule)
    )]
public class StoreDomainTestModule : AbpModule
{

}
