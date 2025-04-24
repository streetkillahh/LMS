using LMS.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Добавление контекста базы данных с логированием
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
           .LogTo(Console.WriteLine, LogLevel.Information)); // Логирование EF Core

// Добавление логирования для всего приложения
builder.Logging.ClearProviders(); // Очищаем стандартные провайдеры
builder.Logging.AddConsole(); // Добавляем вывод в консоль

// Добавление контроллеров и Razor Pages
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Конфигурация middleware
if (!app.Environment.IsDeveloper())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();