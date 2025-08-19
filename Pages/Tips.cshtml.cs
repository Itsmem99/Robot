using Microsoft.AspNetCore.Mvc.RazorPages;

public class TipsModel : PageModel
{
    private static readonly string[] Tips = new[]
    {
        "Drick mycket vatten idag!",
        "Ta en kort promenad f�r att rensa tankarna.",
        "S�tt upp sm� m�l och bel�na dig sj�lv.",
        "Gl�m inte att le � det hj�lper hum�ret.",
        "Ta en paus fr�n sk�rmen d� och d�."
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
