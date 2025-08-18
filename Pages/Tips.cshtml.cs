using Microsoft.AspNetCore.Mvc.RazorPages;

public class TipsModel : PageModel
{
    private static readonly string[] Tips = new[]
    {
        "Drick mycket vatten idag!",
        "Ta en kort promenad för att rensa tankarna.",
        "Sätt upp små mål och belöna dig själv.",
        "Glöm inte att le – det hjälper humöret.",
        "Ta en paus från skärmen då och då."
    };

    public string Tip { get; set; }

    public void OnGet()
    {
        Tip = GetRandomTip();
    }

    public void OnPost()
    {
        Tip = GetRandomTip();
    }

    private string GetRandomTip()
    {
        var rnd = new Random();
        return Tips[rnd.Next(Tips.Length)];
    }
}
