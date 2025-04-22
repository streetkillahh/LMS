using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class Question
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Text { get; set; }

        [Required]
        public string CorrectAnswer { get; set; }

        [Required]
        public int QuizId { get; set; } // ID теста

        public virtual Quiz Quiz { get; set; }
    }
}