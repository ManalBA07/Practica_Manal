using CalculatorService.Server.Services;
using CalculatorSvc = global::CalculatorService.Server.Services.CalculatorService;
using JournalSvc = global::CalculatorService.Server.Services.JournalService;

var builder = WebApplication.CreateBuilder(args);

// MVC + API
builder.Services.AddControllersWithViews();

// Session
builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Dependency Injection
builder.Services.AddSingleton<ICalculatorService, CalculatorSvc>();
builder.Services.AddSingleton<IJournalService, JournalSvc>();

var app = builder.Build();


// Crear journal.json si no existe
var journalPath = Path.Combine(Directory.GetCurrentDirectory(), "journal.json");

if (!File.Exists(journalPath))
{
    File.WriteAllText(journalPath, "[]");
}


// Middleware pipeline

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();


// API controllers
app.MapControllers();

// MVC routing
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();