using Čtenářský_deník.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Čtenářský_deník.Pages;

public class AutorDetailModel : PageModel
{
    [BindProperty(SupportsGet = true)] 
    public int Id { get; set; }

    public Autor Data { get; set; }

    readonly ApplicationDbContext DB;

    public AutorDetailModel(ApplicationDbContext db)
    {
        DB = db;
    }
    public async Task OnGet()
    {
        Data = await DB.Autori
             .AsNoTracking()
             .Include(x => x.Knihy)
             .FirstOrDefaultAsync(x => x.Id == Id); 
    }
}
