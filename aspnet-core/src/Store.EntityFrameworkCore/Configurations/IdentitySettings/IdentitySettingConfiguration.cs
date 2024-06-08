using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.IdentitySettings
{
    public class IdentitySettingConfiguration : IEntityTypeConfiguration<IdentitySetting>
    {
        public void Configure(EntityTypeBuilder<IdentitySetting> builder)
        {
            builder.ToTable(StoreConsts.DbTablePrefix + "IdentitySettings",
                    StoreConsts.DbSchema);
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Name).IsRequired().HasMaxLength(200);

        }
    }
}