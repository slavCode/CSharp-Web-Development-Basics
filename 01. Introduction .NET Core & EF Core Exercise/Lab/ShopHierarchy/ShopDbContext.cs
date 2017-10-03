namespace ShopHierarchy
{
    using Microsoft.EntityFrameworkCore;
    using ShopHierarchy.Models;

    public class ShopDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        public DbSet<Salesman> Salesman { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<ItemOrder> ItemsOrders { get; set; }
    
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                @"Server=Y510P;Database=ShopHierarchy;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Salesman>()
                .HasMany(s => s.Customers)
                .WithOne(c => c.Salesman)
                .HasForeignKey(o => o.SalesmanId);

            modelBuilder
                .Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(c => c.CustomerId);

            modelBuilder
                .Entity<Review>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Reviews)
                .HasForeignKey(r => r.CustomerId);

            modelBuilder
                .Entity<ItemOrder>()
                .HasKey(io => new {io.ItemId, io.OrderId});

            modelBuilder
                .Entity<Item>()
                .HasMany(i => i.Orders)
                .WithOne(io => io.Item)
                .HasForeignKey(io => io.ItemId);

            modelBuilder
                .Entity<Order>()
                .HasMany(o => o.Items)
                .WithOne(io => io.Order)
                .HasForeignKey(io => io.OrderId);

            modelBuilder
                .Entity<Item>()
                .HasMany(i => i.Reviews)
                .WithOne(r => r.Item)
                .HasForeignKey(o => o.ItemId);
        }
    }
}
