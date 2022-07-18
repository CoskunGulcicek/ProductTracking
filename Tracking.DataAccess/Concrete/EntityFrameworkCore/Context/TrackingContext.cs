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
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ProductTrackingDb;Trusted_Connection=True;MultipleActiveResultSets=true");//home
            //optionsBuilder.UseSqlServer(@"Server=MSCOSKUNG;Database=ProductTrackingDb;Trusted_Connection=True;MultipleActiveResultSets=true");//w
            //optionsBuilder.UseSqlServer(@"Server=77.245.159.10;Database=coskung1_;Uid=cgadmin;Pwd=59U8eF3zc83M6Cj3;");
            //optionsBuilder.UseSqlServer(@"Server=.\MSSQLSERVER2019;Database=coskung1_;Uid=cgadmin;Pwd=59U8eF3zc83M6Cj3;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasMany(x => x.CustomerProducts).WithOne(x => x.Product).HasForeignKey(x => x.ProductId);
            modelBuilder.Entity<Customer>().HasMany(x => x.CustomerProducts).WithOne(x => x.Customer).HasForeignKey(x => x.CustomerId);

            modelBuilder.ApplyConfiguration(new CustomerProductMap());
            modelBuilder.ApplyConfiguration(new ProductMap());
            modelBuilder.ApplyConfiguration(new CustomerMap());
            modelBuilder.ApplyConfiguration(new ListMap());
        }
        public DbSet<List> Lists { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerProduct> CustomerProducts { get; set; }
    }
}
