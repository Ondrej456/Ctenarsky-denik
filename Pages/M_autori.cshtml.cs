using Čtenářský_deník.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Čtenářský_deník.Pages
{
    public class M_AutoriModel : PageModel
    {
        readonly ApplicationDbContext DB;
        public List<Autor> Data { get; set; }

        public M_AutoriModel(ApplicationDbContext db)
        {
            DB = db;
        }

        public async Task OnGetAsync()
        {
            // získat ID přihlášeného uživatele
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // vypsat jen autory patřící tomuto uživateli
            Data = await DB.Autori
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.Prijmeni)
                .ThenBy(x => x.Jmeno)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
