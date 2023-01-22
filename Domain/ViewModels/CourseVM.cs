using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModels
{
    public class CourseVM
    {
        public int Id { get; set; }

        [Required]
        public string CourseName { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
