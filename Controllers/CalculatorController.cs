using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("calculator")]
public class CalculatorController : ControllerBase
{
    private readonly IJournalService _journal;
    public CalculatorController(IJournalService journal) => _journal = journal;

    private string TrackingId => Request.Headers["X-Evi-Tracking-Id"];

    [HttpPost("add")]
    public IActionResult Add([FromBody] ListInput input)
    {
        if (input?.Addends == null || input.Addends.Count < 2) return BadRequest();

        double result = input.Addends.Sum();
        string calculation = $"{string.Join(" + ", input.Addends)} = {result}";

        _journal.Record(TrackingId, "Sum", calculation);
        return Ok(new { Sum = result });
    }

    [HttpPost("div")]
    public IActionResult Divide([FromBody] DivInput input)
    {
        if (input.Divisor == 0) return BadRequest(new { ErrorMessage = "Cannot divide by zero" });

        int quotient = (int)(input.Dividend / input.Divisor);
        int remainder = (int)(input.Dividend % input.Divisor);

        _journal.Record(TrackingId, "Div", $"{input.Dividend} / {input.Divisor} = {quotient} rem {remainder}");
        return Ok(new { Quotient = quotient, Remainder = remainder });
    }

    // ... Implement Sub, Mult, and Sqrt similarly ...
}
