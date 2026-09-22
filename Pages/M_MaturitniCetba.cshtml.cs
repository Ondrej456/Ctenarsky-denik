using Čtenářský_deník.Data;
using Čtenářský_deník.Migrations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Čtenářský_deník.Pages
{
    public class M_MaturitniCetbaModel : PageModel
    {
        readonly ApplicationDbContext DB;

        public M_MaturitniCetbaModel(ApplicationDbContext db)
        {
            DB = db;
        }

        [BindProperty(SupportsGet = true)]
        public int IdObdobi { get; set; }
        public Dictionary <int, List<Kniha>> KnihyPodleObdobi { get; set; }
        public List <ObdobiMaturita> Obdobi { get; set; }
        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // načti všechna období
            Obdobi = await DB.ObdobiMaturita
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .ToListAsync();

            // načti všechny knihy přihlášeného uživatele
            var knihy = await DB.Knihy
                .Where(x => x.UserId == userId && x.ObdobiMaturitaId != null)
                .Include(x => x.Autor)
                .AsNoTracking()
                .ToListAsync();

            // seskupení knih podle období
            KnihyPodleObdobi = knihy
                .GroupBy(k => k.ObdobiMaturitaId.Value)
                .ToDictionary(g => g.Key, g => g.ToList());
        }
    }
}
