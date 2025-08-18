using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Robot.Models;

public class MittHumorModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public MittHumorModel(AppDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public List<MoodEntry> MoodEntries { get; set; }
    [BindProperty] public MoodEntry NewMood { get; set; }
    public bool IsAdmin { get; set; }

    [TempData] public string StatusMessage { get; set; }  // Meddelande till användaren

    public async Task OnGetAsync()
    {
        MoodEntries = await _context.MoodEntries.ToListAsync();

        var user = await _userManager.GetUserAsync(User);
        IsAdmin = user != null && user.Email == "Admin@Robot.se";
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid) return Page();

        _context.MoodEntries.Add(NewMood);
        await _context.SaveChangesAsync();

        StatusMessage = "Humörpost tillagd!";
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null || user.Email != "Admin@Robot.se")
            return Forbid();

        var mood = await _context.MoodEntries.FindAsync(id);
        if (mood != null)
        {
            _context.MoodEntries.Remove(mood);
            await _context.SaveChangesAsync();
            StatusMessage = "Humörpost borttagen!";
        }

        return RedirectToPage();
    }
}
