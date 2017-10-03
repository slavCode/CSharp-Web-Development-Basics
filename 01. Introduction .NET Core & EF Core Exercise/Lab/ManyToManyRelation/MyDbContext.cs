namespace ManyToManyRelation
{
    using ManyToManyRelation.Models;
    using Microsoft.EntityFrameworkCore;

    public class MyDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=Y510P;Database=ManyToManyRelation;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentsCourses>()
                .HasKey(sc => new {sc.StudentId, sc.CourseId});

            modelBuilder.Entity<StudentsCourses>()
                .HasOne(sc => sc.Student)
                .WithMany(s => s.Courses)
                .HasForeignKey(sc => sc.StudentId);

            modelBuilder.Entity<Course>()
                .HasMany(c => c.Students);
        }
    }
}
