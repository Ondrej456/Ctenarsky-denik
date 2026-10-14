using Čtenářský_deník.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Čtenářský_deník.Components;
public class PrehledKnihViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(PrehledKnihData data, string pismeno = null, string druh = null )
    {
        if (!string.IsNullOrWhiteSpace(pismeno))
        {
            data.Knihy = data.Knihy
            .Where(k => k.Nazev.StartsWith(pismeno, StringComparison.OrdinalIgnoreCase))
            .ToList();
        }
        if (!string.IsNullOrWhiteSpace(druh))
        {
            data.Knihy = data.Knihy
            .Where(k => k.Druh != null && k.Druh.Contains(druh, StringComparison.OrdinalIgnoreCase))
            .ToList();
        }
       
        return View(data);
   }
}
public class PrehledKnihData
{
    public static readonly string[] Pismena = { "A", "B", "C", "Č", "D", "E", "F", "G", "H", "CH", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "Ř", "S", "Š", "T", "U", "V", "W", "X", "Y", "Z", "Ž" }; // pole pro abecední vyhledávání

    public static readonly string[] Druhy = {"Próza", "Poezie", "Drama" }; // pole druhů
    public ICollection<Kniha> Knihy { get; set; } // pro Moje knihy
    public Dictionary<int, List<Kniha>> KnihyPodleObdobi { get; set; } // pro Maturitní četbu
    public List<ObdobiMaturita> Obdobi { get; set; } // názvy období

    public int AutorId { get; set; }

    public string Pismeno { get; set; }

    public string Druh { get; set; }
}

