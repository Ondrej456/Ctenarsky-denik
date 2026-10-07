using Čtenářský_deník.Data;
using Microsoft.AspNetCore.Mvc;

namespace Čtenářský_deník.Components;
public class PrehledKnihViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(PrehledKnihData data, string? pismeno = null)
    {
        if (!string.IsNullOrWhiteSpace(pismeno))
        {
            data.Knihy = data.Knihy
            .Where(k => k.Nazev.StartsWith(pismeno, StringComparison.OrdinalIgnoreCase))
            .ToList();
        }

        return View(data);
   }
}
public class PrehledKnihData
{
    public static readonly string[] Pismena = { "A", "B", "C", "Č", "D", "E", "F", "G", "H", "CH", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "Ř", "S", "Š", "T", "U", "V", "W", "X", "Y", "Z", "Ž" };
    public ICollection<Kniha> Knihy { get; set; } // pro Moje knihy
    public Dictionary<int, List<Kniha>> KnihyPodleObdobi { get; set; } // pro Maturitní četbu
    public List<ObdobiMaturita> Obdobi { get; set; } // názvy období
}

