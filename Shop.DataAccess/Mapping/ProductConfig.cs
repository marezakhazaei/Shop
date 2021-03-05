using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Common.Entities;
using Shop.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.DataAccess.Mapping
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");
            builder.HasKey(e => e.Id);
            builder.Property(p => p.Title).HasMaxLength(50);
            builder.Property(p => p.ProductDesc).HasMaxLength(255);
            builder.Property(p => p.Photo).HasMaxLength(255);
            builder.Property(p => p.Price);
            builder.Property(p => p.ProductCategory).IsRequired();
            builder.Property(p => p.NumberOfSeat);
            builder.Property(p => p.CreatedDate).HasPrecision(2);
            builder.Property(p => p.CreatedBy).HasMaxLength(50);
            builder.Property(p => p.LastModifiedDate).HasPrecision(2);
            builder.Property(p => p.LastModifiedBy).HasMaxLength(50);

            builder.HasData(new Product { Id = 1, Title = "Small Sofa", ProductDesc = "One Seat", Price = 20.33m, Photo = "one seat1.jpg", ProductCategory = (short)ProductCategory.Sofa, NumberOfSeat = 1 });
            builder.HasData(new Product { Id = 2, Title = "Medium Sofa", ProductDesc = "One Seat", Price = 22.35m, Photo = "one seat2.jpg", ProductCategory = (short)ProductCategory.Sofa, NumberOfSeat = 1 });
            builder.HasData(new Product { Id = 3, Title = "Big Sofa", ProductDesc = "Two Seat", Price = 25.42m, Photo = "two seat1.jpg", ProductCategory = (short)ProductCategory.Sofa, NumberOfSeat = 2 });
            builder.HasData(new Product { Id = 4, Title = "Very Big Sofa", ProductDesc = "Two Seat", Price = 40.33m, Photo = "two seat2.jpg", ProductCategory = (short)ProductCategory.Sofa, NumberOfSeat = 2 });

            builder.HasData(new Product { Id = 5, Title = "Samll Table", ProductDesc = "Table with Two chair", Price = 10.13m, Photo = "table small.jpg", ProductCategory = (short)ProductCategory.Table });
            builder.HasData(new Product { Id = 6, Title = "Medium Table", ProductDesc = "Table with Three chair", Price = 15.35m, Photo = "table medium.jpg", ProductCategory = (short)ProductCategory.Table });
            builder.HasData(new Product { Id = 7, Title = "Big Table", ProductDesc = "Table with Four chair", Price = 20.80m, Photo = "table big.jpg", ProductCategory = (short)ProductCategory.Table });
        }
    }
}
