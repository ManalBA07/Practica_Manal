using System.Text.Json;
using Manal_Calculator.src.CalculatorService.Server.Models;

namespace Manal_Calculator.src.CalculatorService.Server.Services;

public class JournalService : IJournalService
{
    private readonly string filePath;

    public JournalService()
    {
        filePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "journal.json");
    }

    public List<JournalEntry> GetAll()
    {
        if (!File.Exists(filePath))
            return new List<JournalEntry>();

        var json = File.ReadAllText(filePath);

        if (string.IsNullOrWhiteSpace(json))
            return new List<JournalEntry>();

        return JsonSerializer.Deserialize<List<JournalEntry>>(json)
               ?? new List<JournalEntry>();
    }

    public JournalEntry GetByUser(string trackingId)
    {
        var journal = GetAll();

        return journal.FirstOrDefault(x =>
            x.TrackingId == trackingId);
    }

    public void Save(string trackingId, CalculationResult result)
    {
        var journal = GetAll();

        var user = journal.FirstOrDefault(x =>
            x.TrackingId == trackingId);

        if (user == null)
        {
            user = new JournalEntry
            {
                TrackingId = trackingId,
                Calculations = new List<CalculationResult>()
            };

            journal.Add(user);
        }

        user.Calculations.Add(result);

        var json = JsonSerializer.Serialize(
            journal,
            new JsonSerializerOptions
            {
                WriteIndented = true
            });

        File.WriteAllText(filePath, json);
    }
}