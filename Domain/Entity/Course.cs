using System.ComponentModel.DataAnnotations;

namespace Domain.Entity
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        public string CourseName { get; set; }

        public string Description { get; set; }
    }
}
