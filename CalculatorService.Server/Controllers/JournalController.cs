using Microsoft.AspNetCore.Mvc;
using CalculatorService.Server.Services;

namespace CalculatorService.Server.Controllers;

public class JournalController : Controller
{
    private readonly IJournalService journal;

    public JournalController(IJournalService journal)
    {
        this.journal = journal;
    }

    public IActionResult Index()
    {
        var data = journal.GetAll();

        return View(data);
    }
}