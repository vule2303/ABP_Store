using Volo.Abp.Modularity;

namespace Store.Public;

[DependsOn(
    typeof(StorePublicApplicationModule),
    typeof(StoreDomainTestModule)
    )]
public class StorePublicApplicationTestModule : AbpModule
{

}
