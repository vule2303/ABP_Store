using System.Threading.Tasks;

namespace Store.Data;

public interface IStoreDbSchemaMigrator
{
    Task MigrateAsync();
}
