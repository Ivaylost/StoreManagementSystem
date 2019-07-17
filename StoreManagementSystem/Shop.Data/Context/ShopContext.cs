using Microsoft.EntityFrameworkCore;
using StoreManagementSystem.Data.Configurations;
using StoreManagementSystem.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace StoreManagementSystem.Data.Context
{
    public class ShopContext: DbContext
    {

        public ShopContext()
        {
        }

        public ShopContext(DbContextOptions options)
            : base (options)
        {
        }


        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<ProductSupplier> ProductSuppliers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new OrderProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductSupplierConfiguration());

            //modelBuilder.Entity<OrderProduct>()
            //    .HasKey(o => new { o.OrderId, o.ProductId });

            //modelBuilder.Entity<ProductSuplier>()
            //    .HasKey(p => new { p.SuplierId, p.ProductId });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-DU5TDCV\SQLEXPRESS;Initial Catalog=TutorialsTeam;Integrated Security=True");
               //optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-AA9GK3T\SQLEXPRESS;Initial Catalog=SMSDatabase;Integrated Security=True");
            }
        }
    }
}
