using System.Net.Http.Json;

var client = new HttpClient();

client.BaseAddress = new Uri("https://localhost:5001");

client.DefaultRequestHeaders.Add("X-Evi-Tracking-Id", "Juan");

var response = await client.PostAsJsonAsync(
    "/api/calculator/add",
    new { Operand1 = 10, Operand2 = 5 });

var result = await response.Content.ReadAsStringAsync();

Console.WriteLine(result);