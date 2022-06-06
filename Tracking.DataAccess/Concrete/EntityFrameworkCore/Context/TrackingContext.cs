using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracking.DataAccess.Concrete.EntityFrameworkCore.Mapping;
using Tracking.Entities.Concrete;

namespace Tracking.DataAccess.Concrete.EntityFrameworkCore.Context
{
    public class TrackingContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ProductTrackingDb;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Basket>().HasOne(x => x.Customer).WithOne(x => x.Basket).HasForeignKey<Basket>(x=>x.CustomerId);

            modelBuilder.Entity<Product>().HasMany(x => x.BasketProducts).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<Basket>().HasMany(x => x.BasketProducts).WithOne(x => x.Basket).HasForeignKey(x => x.BasketId);

            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketProduct> BasketProducts { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
