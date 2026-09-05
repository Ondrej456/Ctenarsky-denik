using Čtenářský_deník.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Čtenářský_deník.Pages;

public class KnihyModel : PageModel
{
    readonly ApplicationDbContext DB;
    public List<Kniha> Data { get; set; } = new();

    public KnihyModel(ApplicationDbContext db)
    {
        DB = db;
    }
    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) // nepřihlášený uvidí pouze obecné autory na stránce autoři
        {
            Data = new List<Kniha>();
            Data = await DB.Knihy
            .Where(x => x.UserId == null)
            .Include(x => x.Autor)
            .OrderBy(x => x.Nazev)
            .ToListAsync();
        }
        else
        {
            Data = new List<Kniha>();
            Data = await DB.Knihy
                .Where(x => x.UserId == userId)
                .AsNoTracking()
                .Include(x => x.Autor)
                .OrderBy(x => x.Nazev)
                .ToListAsync();
        }

    }
}
