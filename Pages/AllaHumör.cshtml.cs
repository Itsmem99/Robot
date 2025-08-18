using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Robot.Models;

namespace Robot;

public class AllaHumorModel : PageModel
{
    private readonly AppDbContext _context;

    public AllaHumorModel(AppDbContext context)
    {
        _context = context;
    }

    public List<MoodEntry>? MoodEntries { get; set; }

    public void OnGet()
    {
        MoodEntries = null; // Visa inget först
    }

    public async Task OnPostAsync()
    {
        MoodEntries = await _context.MoodEntries.ToListAsync();
    }
}
