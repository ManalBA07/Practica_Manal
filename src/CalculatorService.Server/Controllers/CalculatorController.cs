using Microsoft.AspNetCore.Mvc;
using Manal_Calculator.src.CalculatorService.Server.Models;
using Manal_Calculator.src.CalculatorService.Server.Services;
using System.Security.Cryptography.Xml;

namespace Manal_Calculator.src.CalculatorService.Server.Controllers;

[ApiController]
[Route("api/calculator")]
public class CalculatorController : ControllerBase
{
    private readonly ICalculatorService calculator;
    private readonly IJournalService journal;

    public CalculatorController(
        ICalculatorService calculator,
        IJournalService journal)
    {
        this.calculator = calculator;
        this.journal = journal;
    }

    private void Track(string operation, double result)
    {
        var trackingId = Request.Headers["X-Evi-Tracking-Id"]
            .FirstOrDefault();

        if (!string.IsNullOrEmpty(trackingId))
        {
            journal.Save(trackingId, new CalculationResult
            {
                Operation = operation,
                Result = result,
                Timestamp = DateTime.UtcNow
            });
        }
    }
   

    [HttpPost("add")]
    public IActionResult Add([FromBody] CalculationRequest req)
    {
        var result = calculator.Add(req.Operand1, req.Operand2);

        Track("ADD", result);

        return Ok(result);
    }

    [HttpPost("sub")]
    public IActionResult Sub([FromBody] CalculationRequest req)
    {
        var result = calculator.Sub(req.Operand1, req.Operand2);

        Track("SUB", result);

        return Ok(result);
    }

    [HttpPost("mul")]
    public IActionResult Mul([FromBody] CalculationRequest req)
    {
        var result = calculator.Mul(req.Operand1, req.Operand2);

        Track("MUL", result);

        return Ok(result);
    }

    [HttpPost("div")]
    public IActionResult Div([FromBody] CalculationRequest req)
    {
        var result = calculator.Div(req.Operand1, req.Operand2);

        Track("DIV", result);

        return Ok(result);
    }

    [HttpPost("sqrt")]
    public IActionResult Sqrt([FromBody] double operand)
    {
        var result = calculator.Sqrt(operand);

        Track("SQRT", result);

        return Ok(result);
    }
}