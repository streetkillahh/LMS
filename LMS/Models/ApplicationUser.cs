﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LMS.Models
{
    public class ApplicationUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; } // Хэшированный пароль

        [Required]
        public string Role { get; set; } // Роль: "Admin", "Teacher", "Student"

        public virtual ICollection<Course> Courses { get; set; }
    }
}