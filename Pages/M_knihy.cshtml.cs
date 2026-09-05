using Čtenářský_deník.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
namespace Čtenářský_deník.Pages
{
    public class M_knihyModel : PageModel
    {
        readonly ApplicationDbContext DB;
        public List<Kniha> Data { get; set; }

        public M_knihyModel(ApplicationDbContext db)
        {
            DB = db;
        }
        public async Task OnGetAsync()
        {
            // získat ID přihlášeného uživatele
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // vypsat jen knihy patřící tomuto uživateli
            Data = await DB.Knihy
                .Where(x => x.UserId == userId)
                .Include(x => x.Autor)
                .OrderBy(x => x.Nazev)
                .ThenBy(x => x.Id)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
