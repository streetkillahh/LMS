using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LMS.Models
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public int LessonId { get; set; } // ID урока

        public virtual Lesson Lesson { get; set; }

        [JsonIgnore] // Чтобы избежать рекурсии при сериализации
        public virtual ICollection<Question> Questions { get; set; }
    }
}