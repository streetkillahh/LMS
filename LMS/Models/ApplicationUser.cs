﻿using System.ComponentModel.DataAnnotations;

namespace LMS.Models;

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
    public string Role { get; set; } // Администратор, Учитель, Студент
}