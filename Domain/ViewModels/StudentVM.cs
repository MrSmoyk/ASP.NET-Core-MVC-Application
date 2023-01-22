using System;
using System.ComponentModel.DataAnnotations;


namespace Domain.ViewModels
{
    public class StudentVM
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a group!")]
        public int GroupId { get; set; }

        public virtual GroupVM Group { get; set; }

    }
}
