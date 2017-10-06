namespace BankSystem.Data
{
    using BankSystem.Models;
    using Microsoft.EntityFrameworkCore;

    public class BankSystemDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<SavingAccount> SavingAccounts { get; set; }

        public DbSet<CheckingAccount> CheckingAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=Y510P;Database=BankSystem;Integrated Security=True;");

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasMany(u => u.CheckingAccounts)
                .WithOne(ca => ca.User)
                .HasForeignKey(u => u.UserId);

            builder.Entity<User>()
                .HasMany(u => u.SavingAccounts)
                .WithOne(sa => sa.User)
                .HasForeignKey(u => u.UserId);

            base.OnModelCreating(builder);
        }
    }
}
