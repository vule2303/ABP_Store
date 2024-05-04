using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Store.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Configurations.Products
{
    public class ProductAttributeVarcharConfiguration : IEntityTypeConfiguration<ProductAttributeVarchar>
    {
        public void Configure(EntityTypeBuilder<ProductAttributeVarchar> builder)
        {
            builder.ToTable(StoreConsts.DbTablePrefix + "ProductAttributeVarchars");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Value).HasMaxLength(500);
        }
    }
}
