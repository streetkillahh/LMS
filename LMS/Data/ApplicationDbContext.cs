using LMS.Models;
using Microsoft.EntityFrameworkCore;

namespace LMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Настройка связи между ApplicationUser и Course
            modelBuilder.Entity<Course>()
                .HasOne(c => c.Teacher)
                .WithMany(u => u.Courses)
                .HasForeignKey(c => c.TeacherId);
        }
    }
}