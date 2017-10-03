namespace OneToManyRelation
{
    using OneToManyRelation.Models;

    class StarUp
    {
        static void Main()
        {
            var db = new AppDbContext();
            db.Database.EnsureCreated();

            var department = new Department {Name = "Development"};
            var employee = new Employee { Name = "Pesho" };
            db.Employees.Add(employee);
            db.Departments.Add(department);
            db.SaveChanges();
        }
    }
}