using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Common.Entities;

namespace Shop.DataAccess.Mapping
{
    public class BasketConfig : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.ToTable("Basket");
            builder.HasKey(e => e.Id);
            builder.Property(p => p.ClientId).IsRequired();
            builder.Property(p => p.BasketStatus).IsRequired();
            builder.Property(p => p.TotalCount).IsRequired();
            builder.Property(p => p.TotalDiscount).IsRequired();
            builder.Property(p => p.TotalPrice).IsRequired();
            builder.Property(p => p.ProductPrices).IsRequired();
            builder.Property(p => p.CreatedDate).HasPrecision(2);
            builder.Property(p => p.CreatedBy).HasMaxLength(50);
            builder.Property(p => p.LastModifiedDate).HasPrecision(2);
            builder.Property(p => p.LastModifiedBy).HasMaxLength(50);

            builder.HasMany(m => m.BasketItems).WithOne(o => o.Basket).HasForeignKey(f => f.BasketId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
