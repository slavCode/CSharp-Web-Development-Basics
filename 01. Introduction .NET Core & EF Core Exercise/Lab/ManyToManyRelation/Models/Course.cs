namespace ManyToManyRelation.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Course
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public List<StudentsCourses> Students { get; set; } = new List<StudentsCourses>();
    }
}
