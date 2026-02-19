namespace CalculatorService.Server.Models;

public class JournalEntry
{
    public string TrackingId { get; set; }

    public List<CalculationResult> Calculations { get; set; }
        = new();
}