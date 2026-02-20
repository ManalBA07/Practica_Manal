using Microsoft.AspNetCore.Mvc;
using Manal_Calculator.src.CalculatorService.Server.Services;

namespace Manal_Calculator.src.CalculatorService.Server.Controllers;

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