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
                user.PasswordHash = PasswordHelper.HashPassword(password);
                _context.ApplicationUsers.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login");
            }
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