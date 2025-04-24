using Microsoft.AspNetCore.Mvc;
using LMS.Data;
using LMS.Models;
using LMS.Services;
using Microsoft.EntityFrameworkCore;

namespace LMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: Register
        [HttpPost]
        public async Task<IActionResult> Register(ApplicationUser user, string password)
        {
            if (ModelState.IsValid)
            {
                // Отладочное сообщение
                Console.WriteLine($"Name: {user.Name}, Email: {user.Email}, Role: {user.Role}, Password: {password}");

                // Хэшируем пароль
                user.PasswordHash = PasswordHelper.HashPassword(password);

                // Проверяем, что пароль хэширован
                Console.WriteLine($"Hashed Password: {user.PasswordHash}");

                try
                {
                    // Добавляем пользователя в базу данных
                    _context.ApplicationUsers.Add(user);
                    await _context.SaveChangesAsync();

                    // Отладочное сообщение
                    Console.WriteLine("User successfully registered.");

                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    // Логирование ошибок
                    Console.WriteLine($"Error saving user to database: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred while saving the user.");
                }
            }

            // Если модель недействительна, возвращаем представление с ошибками
            return View(user);
        }

        // GET: Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.ApplicationUsers.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null && PasswordHelper.VerifyPassword(password, user.PasswordHash))
            {
                HttpContext.Response.Cookies.Append("UserId", user.Id.ToString());
                Console.WriteLine($"User logged in: {user.Email}");
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid email or password.");
            return View();
        }

        // GET: Logout
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete("UserId");
            return RedirectToAction("Index", "Home");
        }
    }
}