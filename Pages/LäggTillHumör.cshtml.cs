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

    [BindProperty] public string Emoji { get; set; } = "";
    [BindProperty] public string Text { get; set; } = "";
    public string? Message { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(Emoji) || string.IsNullOrWhiteSpace(Text))
        {
            Message = "Fyll i både emoji och text!";
            return Page();
        }

        var mood = new MoodEntry { Emoji = Emoji, Text = Text };

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

        try
        {
            var client = _httpClientFactory.CreateClient("MyApi");
            var response = await client.PostAsJsonAsync("api/messages", mood);

            if (response.IsSuccessStatusCode)
                Message += " Meddelandet skickades också till API:t!";
            else
            {
                var errorText = await response.Content.ReadAsStringAsync();
                Message += $" Kunde inte skicka till API:t. Status: {response.StatusCode}. Fel: {errorText}";
            }
        }
        catch (Exception ex)
        {
            Message += $" Fel vid API-anrop: {ex.Message}";
        }

        Emoji = "";
        Text = "";
        return Page();
    }
}
