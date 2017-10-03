namespace OneToManyRelation
{
    using Microsoft.EntityFrameworkCore;
    using OneToManyRelation.Models;

    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=Y510P;Database=TestDb;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(emp => emp.Manager)
                .WithMany(m => m.Employees)
                .HasForeignKey(emp => emp.ManagerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
