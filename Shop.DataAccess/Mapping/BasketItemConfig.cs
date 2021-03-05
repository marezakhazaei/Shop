using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Common.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.DataAccess.Mapping
{
    class BasketItemConfig : IEntityTypeConfiguration<BasketItem>
    {
        public void Configure(EntityTypeBuilder<BasketItem> builder)
        {
            builder.ToTable("BasketItem");
            builder.HasKey(e => e.Id);
            builder.Property(p => p.ProductCount);
            builder.Property(p => p.Price);
            builder.Property(p => p.CreatedDate).HasPrecision(2);
            builder.Property(p => p.CreatedBy).HasMaxLength(50);
            builder.Property(p => p.LastModifiedDate).HasPrecision(2);
            builder.Property(p => p.LastModifiedBy).HasMaxLength(50);

            builder.HasOne(o => o.Basket).WithMany(m => m.BasketItems).HasForeignKey(f => f.BasketId);
            //builder.HasOne(o => o.Product).WithOne();
        }
    }
}
