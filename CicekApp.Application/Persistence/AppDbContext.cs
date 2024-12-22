using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CicekApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CicekApp.Application.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Flower> Flowers { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Courier> Couriers { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToLower());
                }
            }

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Delivery)
                .WithOne(d => d.Order)
                .HasForeignKey<Delivery>(d => d.OrderId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role) // User bir Role'e ait
                .WithMany(r => r.Users) // Role birçok User'a sahip
                .HasForeignKey(u => u.RoleId); // Foreign Key tanımı

            modelBuilder.Entity<Flower>()
                    .HasOne(f => f.Category)
                    .WithMany(c => c.Flowers)
                    .HasForeignKey(f => f.CategoryId);

            // Set table names to lowercase
            modelBuilder.Entity<Customer>().ToTable("customers", "public");
            modelBuilder.Entity<Category>().ToTable("category", "public");

            modelBuilder.Entity<Order>().ToTable("orders", "public");
            modelBuilder.Entity<Flower>().ToTable("flowers", "public");
            modelBuilder.Entity<Delivery>().ToTable("deliveries", "public");
            modelBuilder.Entity<Courier>().ToTable("couriers", "public");
            modelBuilder.Entity<User>().ToTable("users", "public");
            modelBuilder.Entity<Role>().ToTable("roles", "public");
            base.OnModelCreating(modelBuilder);

        }


    }

}