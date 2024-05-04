using Store.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Store.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(StoreEntityFrameworkCoreModule),
    typeof(StoreApplicationContractsModule)
    )]
public class StoreDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
