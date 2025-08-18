using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Robot.Models;

public class AdminPanelModel : PageModel
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly AppDbContext _context;

    public AdminPanelModel(UserManager<IdentityUser> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public bool IsAdmin { get; set; }
    public int TotalMoods { get; set; }
    public List<(string Emoji, int Count)> MoodsPerEmoji { get; set; }
    public List<MoodEntry> LatestMoods { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        IsAdmin = user != null && user.Email == "Admin@Robot.se";

        if (!IsAdmin)
            return Forbid();

        TotalMoods = await _context.MoodEntries.CountAsync();

        MoodsPerEmoji = await _context.MoodEntries
            .GroupBy(m => m.Emoji)
            .Select(g => new { Emoji = g.Key, Count = g.Count() })
            .AsNoTracking()
            .ToListAsync()
            .ContinueWith(t => t.Result.Select(x => (x.Emoji, x.Count)).ToList());

        LatestMoods = await _context.MoodEntries
            .OrderByDescending(m => m.Id)
            .Take(5)
            .ToListAsync();

        return Page();
    }
}
