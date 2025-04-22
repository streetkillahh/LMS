using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public int TeacherId { get; set; } // ID преподавателя

        public virtual ApplicationUser Teacher { get; set; }
    }
}