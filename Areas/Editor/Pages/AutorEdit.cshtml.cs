using Čtenářský_deník.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Čtenářský_deník.Areas.Editor.Pages;

public class AutorEditModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int IdAutora { get; set; }

    [BindProperty]
    public Autor Data { get; set; }

    [BindProperty]
    public IFormFile Image { get; set; } // načtení obrázku

    readonly ApplicationDbContext DB;

    public AutorEditModel(ApplicationDbContext db)
    {
        DB = db;
    }

    public async Task OnGetAsync()
    {
        if (IdAutora == 0)
        {
            Data = new Autor();
        }
        else
        {
            Data = await DB.Autori.FindAsync(IdAutora);
        }
    }
    public async Task<IActionResult> OnPostAsync() // uložit autora
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        if (Image != null && Image.Length > 0)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(Image.FileName)}";

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot/uploads/autor",
                fileName
            );

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
            }

            Data.ImagePath = $"/uploads/autor/{fileName}";
        }

        if (IdAutora != Data?.Id) { return BadRequest(); }

        if (IdAutora == 0)
        {
           await DB.Autori.AddAsync(Data);
        }
        else
        {
            DB.Autori.Update(Data);
        }
        // 1) Získat ID přihlášeného uživatele
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // 2) Přiřadit ho k autorovi
        Data.UserId = userId;

        await DB.SaveChangesAsync();
        return Redirect($"/autor/{Data.Id}");
    }

    public async Task OnPostVymazatAsync(string id_S_Autora)
    {
        if(IdAutora != Convert.ToInt32 (id_S_Autora))
        {
            return;
        }
        else 
        {
           DB.Autori.Remove(await DB.Autori.FindAsync(IdAutora));
            await DB.SaveChangesAsync();
            Response.Redirect($"/autori");
        }
    }


}
