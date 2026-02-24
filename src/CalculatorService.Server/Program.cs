using Manal_Calculator.src.CalculatorService.Server.Services;
using CalculatorSvc = global::Manal_Calculator.src.CalculatorService.Server.Services.CalculatorService;
using JournalSvc = global::Manal_Calculator.src.CalculatorService.Server.Services.JournalService;

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
// Register concrete implementations directly so the DI container can resolve
// the interfaces used by controllers.
builder.Services.AddSingleton<Manal_Calculator.src.CalculatorService.Server.Services.ICalculatorService, Manal_Calculator.src.CalculatorService.Server.Services.CalculatorService>();
builder.Services.AddSingleton<Manal_Calculator.src.CalculatorService.Server.Services.IJournalService, Manal_Calculator.src.CalculatorService.Server.Services.JournalService>();

// Reflection-based fallback registration: register any interfaces named
// ICalculatorService / IJournalService with corresponding implementations
// named CalculatorService / JournalService from loaded assemblies. This
// covers cases where duplicate folders/namespaces exist (e.g. "Server.*" vs
// "Manal_Calculator.src.*").
{
    var allTypes = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(a => { try { return a.GetTypes(); } catch { return Array.Empty<Type>(); } });

    foreach (var ifaceName in new[] { "ICalculatorService", "IJournalService" })
    {
        var iface = allTypes.FirstOrDefault(t => t.IsInterface && t.Name == ifaceName);
        if (iface == null)
            continue;

        var implName = ifaceName.StartsWith("I") ? ifaceName.Substring(1) : ifaceName;
        var impl = allTypes.FirstOrDefault(t => t.IsClass && !t.IsAbstract && t.Name == implName && iface.IsAssignableFrom(t));
        if (impl != null)
        {
            builder.Services.AddSingleton(iface, impl);
        }
    }
}

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