using Microsoft.EntityFrameworkCore;
using CicekApp.Domain.Entities;

namespace CicekApp.Application.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // Define the DbSets
        public DbSet<Order> Orders { get; set; }
        public DbSet<Flower> Flowers { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartFlowers> CartFlowers { get; set; }  // Cart_Flower tablosunu ekliyoruz

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Set all property names to lowercase
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entity.GetProperties())
                {
                    property.SetColumnName(property.Name.ToLower());
                }
            }

            // Relationship between Order and User
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User) // Order belongs to User
                .WithMany(u => u.Orders) // User has many Orders
                .HasForeignKey(o => o.UserId); // ForeignKey definition

            // Relationship between Order and Delivery
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Delivery) // Order has one Delivery
                .WithOne(d => d.Order) // Delivery belongs to one Order
                .HasForeignKey<Delivery>(d => d.OrderId); // ForeignKey definition

            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Order) // Cart bir Order'a sahip olabilir
                .WithOne(o => o.Cart) // Order bir Cart'a bağlı
                .HasForeignKey<Order>(o => o.CartId) // CartId foreign key'i
                .OnDelete(DeleteBehavior.SetNull);

            // Relationship between Delivery and Courier
            modelBuilder.Entity<Delivery>()
                .HasOne(d => d.Courier) // Delivery has one Courier
                .WithMany(c => c.Deliveries) // Courier has many Deliveries
                .HasForeignKey(d => d.CourierId); // ForeignKey definition

            // Relationship between Flower and Category
            modelBuilder.Entity<Flower>()
                .HasOne(f => f.Category) // Flower belongs to Category
                .WithMany(c => c.Flowers) // Category has many Flowers
                .HasForeignKey(f => f.CategoryId); // ForeignKey definition

            // Relationship between User and Role
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role) // User has one Role
                .WithMany(r => r.Users) // Role has many Users
                .HasForeignKey(u => u.RoleId); // ForeignKey definition

            // Relationship between Cart and User
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.User) // Cart belongs to User
                .WithMany(u => u.Carts) // User has many Carts
                .HasForeignKey(c => c.UserId); // ForeignKey definition


            // Relationship between Cart and Order (Cart has many Orders, not a one-to-one)
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Order) // Cart belongs to Order
                .WithOne(o => o.Cart) // Order has one Cart (each order has only one cart)
                .HasForeignKey<Cart>(c => c.OrderId); // ForeignKey definition

            // Relationship between Cart and CartFlower
            modelBuilder.Entity<CartFlowers>()
                .HasKey(cf => new { cf.CartId, cf.FlowerId }); // Composite primary key
            modelBuilder.Entity<CartFlowers>()
                .HasOne(cf => cf.Cart)  // CartFlower belongs to Cart
                .WithMany(c => c.CartFlowers)  // Cart has many CartFlowers
                .HasForeignKey(cf => cf.CartId);  // ForeignKey definition

            modelBuilder.Entity<CartFlowers>()
                .HasOne(cf => cf.Flower)  // CartFlower belongs to Flower
                .WithMany(f => f.CartFlowers)  // Flower has many CartFlowers
                .HasForeignKey(cf => cf.FlowerId);  // ForeignKey definition

            // Set table names to lowercase and add schema as needed
            modelBuilder.Entity<Order>().ToTable("orders", "public");
            modelBuilder.Entity<Flower>().ToTable("flowers", "public");
            modelBuilder.Entity<Delivery>().ToTable("deliveries", "public");
            modelBuilder.Entity<Courier>().ToTable("couriers", "public");
            modelBuilder.Entity<User>().ToTable("users", "public");
            modelBuilder.Entity<Role>().ToTable("roles", "public");
            modelBuilder.Entity<Category>().ToTable("categories", "public");
            modelBuilder.Entity<Cart>().ToTable("carts", "public");
            modelBuilder.Entity<CartFlowers>().ToTable("cart_flowers", "public");  // Cart_Flower tablosu ekleniyor


            base.OnModelCreating(modelBuilder);
        }
    }
}
