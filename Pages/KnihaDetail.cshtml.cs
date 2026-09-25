using Čtenářský_deník.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Čtenářský_deník.Pages
{
    public class KnihaDetailModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }
        public Kniha Kniha { get; set; }

        readonly ApplicationDbContext DB;

        public KnihaDetailModel(ApplicationDbContext db)
        {
            DB = db;
        }
        public async Task OnGetAsync()
        {
                Kniha = await DB.Knihy
                 .AsNoTracking()
                 .Include(x => x.Autor)     // načtení autorů
                 .Include(x => x.Images)   // načtení cest k obrázkům
                 .FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}
