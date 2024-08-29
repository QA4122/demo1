using Microsoft.EntityFrameworkCore;
using Shopdemo1.Models;

namespace Shopdemo1.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(10,2)");
            modelBuilder.Entity<Order>()
                .Property(p => p.TotalAmount)
                .HasColumnType("decimal(10,2)");
            modelBuilder.Entity<OrderItem>()
                .Property(p => p.UnitPrice)
                .HasColumnType("decimal(10,2)");
            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasColumnType("decimal(10,2)");

        }
        public DbSet<Product> products {get; set;}
        public DbSet<Category> categories {get; set;}
        public DbSet<Profile> profiles {get; set;}
        public DbSet<Order> orders {get; set;}
        public DbSet<OrderItem> orderItems {get; set;}
        public DbSet<Cart> carts {get; set;}
        public DbSet<CartItem> cartItems {get; set;}
        public DbSet<Payment> payments {get; set;}
        public DbSet<Shipment> shipments {get; set;}
        public DbSet<Wishlist> wishlists {get; set;}
        public DbSet<Account> accounts {get; set;}
        public DbSet<Image> images {get; set;}
    }
}
