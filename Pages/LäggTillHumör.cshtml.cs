using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Robot.Models;

namespace Robot;

public class LäggTillHumörModel(AppDbContext context) : PageModel
{
    private readonly AppDbContext _context = context;

    [BindProperty]
    public string Emoji { get; set; } = "";

    [BindProperty]
    public string Text { get; set; } = "";

    public string? Message { get; set; }

    public void OnGet()
    {
        // Ingenting behöver göras på
        // 
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(Emoji) || string.IsNullOrWhiteSpace(Text))
        {
            Message = "Fyll i både emoji och text!";
            return Page();
        }

        var mood = new MoodEntry
        {
            Emoji = Emoji,
            Text = Text
        };

        _context.MoodEntries.Add(mood);

        Message = "Humör sparat!";
        Emoji = "";
        Text = "";

        return Page();
    }
}
