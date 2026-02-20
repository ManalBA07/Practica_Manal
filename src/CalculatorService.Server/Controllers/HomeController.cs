using Microsoft.AspNetCore.Mvc;

namespace Manal_Calculator.src.CalculatorService.Server.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
        => View();

    [HttpPost]
    public IActionResult Enter(string username)
    {
        HttpContext.Session.SetString("User", username ?? "Guest");

        return RedirectToAction("Menu");
    }

    public IActionResult Menu()
        => View();

    public IActionResult Journal()
        => View();
}