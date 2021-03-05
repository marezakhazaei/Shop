using Microsoft.EntityFrameworkCore;
using Shop.Common.Entities;
using Shop.DataAccess.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.DataAccess
{
    public class ShopDbContext : DbContext
    {
        public ShopDbContext(DbContextOptions<ShopDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductConfig());
            modelBuilder.ApplyConfiguration(new BasketConfig());
            modelBuilder.ApplyConfiguration(new BasketItemConfig());
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
    }
}
