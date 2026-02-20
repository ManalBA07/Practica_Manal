using Manal_Calculator.src.CalculatorService.Server.Models;

namespace Manal_Calculator.src.CalculatorService.Server.Services;

public interface IJournalService
{
    void Save(string trackingId, CalculationResult result);
    List<JournalEntry> GetAll();
    JournalEntry GetByUser(string trackingId);
}