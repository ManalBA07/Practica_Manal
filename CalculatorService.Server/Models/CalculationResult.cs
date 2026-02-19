namespace CalculatorService.Server.Models;

public class CalculationResult
{
    public double Result { get; set; }
    public string Operation { get; set; }
    public DateTime Timestamp { get; set; }
}