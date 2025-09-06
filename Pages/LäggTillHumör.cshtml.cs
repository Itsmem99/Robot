using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Robot.Models;
using System.Net.Http.Json;

namespace Robot;

public class LäggTillHumörModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly IHttpClientFactory _httpClientFactory;

    public LäggTillHumörModel(AppDbContext context, IHttpClientFactory httpClientFactory)
    {
        _context = context;
        _httpClientFactory = httpClientFactory;
    }

    [BindProperty]
    public string Emoji { get; set; } = "";

    [BindProperty]
    public string Text { get; set; } = "";

    public string? Message { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(Emoji) || string.IsNullOrWhiteSpace(Text))
        {
            Message = "Fyll i både emoji och text!";
            return Page();
        }

        // Skapa mood-entry
        var mood = new MoodEntry
        {
            Emoji = Emoji,
            Text = Text
        };

        // Försök spara i databasen först
        try
        {
        _context.MoodEntries.Add(mood);
            await _context.SaveChangesAsync();
            Message = "Humör sparat i databasen!";
        }
        catch (Exception ex)
        {
            Message = $"Fel vid sparande i databasen: {ex.Message}";
            return Page();
        }

        // Skicka till API
        try
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri("https://localhost:7207/"); // API-adress

            var payload = new
            {
                Emoji = Emoji,
                Text = Text
            };

            var response = await client.PostAsJsonAsync("api/messages", payload);

            if (response.IsSuccessStatusCode)
            {
                Message += " Meddelandet skickades också till API:t!";
            }
            else
            {
                Message += " Kunde inte skicka till API:t.";
            }
        }
        catch (Exception ex)
        {
            Message += $" Fel vid API-anrop: {ex.Message}";
        }

        // Nollställ formuläret
        Emoji = "";
        Text = "";

        return Page();
    }
}
