using CalculatorService.Server.Models;

namespace CalculatorService.Server.Services;

public interface IJournalService
{
    void Save(string trackingId, CalculationResult result);
    List<JournalEntry> GetAll();
    JournalEntry GetByUser(string trackingId);
}