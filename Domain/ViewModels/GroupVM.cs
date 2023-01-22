using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModels
{
    public class GroupVM
    {
        public int Id { get; set; }

        [Required]
        public string GroupName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a course!")]
        public int CourseId { get; set; }

        public virtual CourseVM Course { get; set; }

    }
}
