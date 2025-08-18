using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System;

public class IndexModel : PageModel
{
    // En statisk lista med inspirerande citat som visas slumpmässigt
    private static readonly string[] Quotes = new[]
    {
        "Varje dag är en ny möjlighet.",
        "Tro på dig själv!",
        "Ge aldrig upp, framgång kommer.",
        "Ett steg i taget.",
        "Styrka kommer inte från att alltid vinna, utan från att resa sig efter att ha fallit.",
        "Håll fokus på det du kan påverka.",
        "Små framsteg varje dag leder till stora resultat.",
        "Ta hand om dig själv, du är viktig."
    };

    // Egna properties som binder till formulärfält i vyn
    public required string UserInput { get; set; }
    public required string ResponseMessage { get; set; }

    // När sidan laddas första gången (GET)
    public void OnGet()
    {
        // Visa ett slumpmässigt citat på sidan
        ViewData["Quote"] = GetRandomQuote();

        // Initiera ResponseMessage till en välkomsttext
        ResponseMessage = "Välkommen! Skriv något om hur du känner dig idag.";
    }

    // När formuläret skickas (POST)
    public void OnPost()
    {
        // Visa ett nytt slumpmässigt citat även efter post
        ViewData["Quote"] = GetRandomQuote();

        // Läs in det användaren skrev i formuläret
        UserInput = Request.Form["UserInput"];

        // Kontrollera om användaren inte skrev något alls
        if (string.IsNullOrWhiteSpace(UserInput))
        {
            ResponseMessage = "Skriv gärna något så jag kan hjälpa dig bättre!";
            return;
        }

        // Gör texten till gemener för enklare jämförelser
        var text = UserInput.ToLower();

        // Enkel ord-baserad logik för att ge svar beroende på hur användaren mår
        if (text.Contains("trött") || text.Contains("tired") || text.Contains("sliten"))
        {
            ResponseMessage = "Ta en paus och andas djupt 🌿 Det är viktigt att vila.";
        }
        else if (text.Contains("glad") || text.Contains("lycklig") || text.Contains("bra"))
        {
            ResponseMessage = "Härligt att höra! Fortsätt att sprida glädje 😊";
        }
        else if (text.Contains("stress") || text.Contains("stressad") || text.Contains("orolig"))
        {
            ResponseMessage = "Kom ihåg att ta regelbundna pauser och koppla av 🧘 Det hjälper mycket.";
        }
        else if (text.Contains("ledsen") || text.Contains("sorgsen") || text.Contains("nere"))
        {
            ResponseMessage = "Det är okej att känna så ibland. Om du vill kan du prata med någon du litar på 💙";
        }
        else
        {
            ResponseMessage = "Tack för att du delar med dig! Varje känsla är viktig.";
        }

        // Håll koll på hur många gånger användaren skickat in via session
        int count = HttpContext.Session.GetInt32("SubmitCount") ?? 0;
        count++;
        HttpContext.Session.SetInt32("SubmitCount", count);

        // Du kan även visa antal inlägg i t.ex. ViewData om du vill visa det i vyn
        ViewData["SubmitCount"] = count;
    }

    // Hjälpfunktion för att hämta ett slumpmässigt citat från listan
    private string GetRandomQuote()
    {
        var rnd = new Random();
        return Quotes[rnd.Next(Quotes.Length)];
    }
}
