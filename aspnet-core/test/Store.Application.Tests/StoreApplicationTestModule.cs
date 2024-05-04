using Volo.Abp.Modularity;

namespace Store;

[DependsOn(
    typeof(StoreApplicationModule),
    typeof(StoreDomainTestModule)
    )]
public class StoreApplicationTestModule : AbpModule
{

}
