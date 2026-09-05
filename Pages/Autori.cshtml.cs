using Čtenářský_deník.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Čtenářský_deník.Pages;

public class AutoriModel : PageModel
{
    readonly ApplicationDbContext DB;
    public List<Autor> Data { get; set; }

    public AutoriModel(ApplicationDbContext db) 
    { 
        DB = db;
    }
    public async Task OnGetAsync()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId == null) // nepřihlášený uvidí pouze obecné autory na stránce autoři
        {
            Data = new List<Autor>();   
            Data = await DB.Autori
            .Where(x => x.UserId == null)
           .OrderBy(x => x.Prijmeni)
           .ToListAsync();
        }
        else // přihlášený své autory na stránce Moji autoři
        {
            Data = new List<Autor>();
            Data = await DB.Autori
               .Where(x => x.UserId == userId)
               .OrderBy(x => x.Prijmeni)
               .ToListAsync();
        }
    }
            
    
}
