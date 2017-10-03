namespace OneToManyRelation.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public int? ManagerId { get; set; }
        public Employee Manager { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();
        public Department Department { get; set; }
    }
}
