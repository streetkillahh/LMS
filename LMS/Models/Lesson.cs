using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class Lesson
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Content { get; set; }

        [Required]
        public int CourseId { get; set; } // ID курса

        public virtual Course Course { get; set; }
    }
}