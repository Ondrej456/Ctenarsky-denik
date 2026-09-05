using Čtenářský_deník.Data;
using Microsoft.AspNetCore.Mvc;

namespace Čtenářský_deník.Components;
public class PrehledKnihViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(PrehledKnihData data)
    {
        return View(data);
    }
}
public class PrehledKnihData
{
    public ICollection<Kniha> Knihy { get; set; }
}

