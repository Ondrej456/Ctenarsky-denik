using Čtenářský_deník.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Čtenářský_deník.Areas.Editor.Pages;

public class AutorEditModel : PageModel
{
    [BindProperty(SupportsGet = true)] // načíst z URL řádku za otazníkem, musí se shodovat název
    public int IdAutora { get; set; }

    [BindProperty(SupportsGet = true)]
    public string Chyba { get; set; }

    [BindProperty]
    public Autor Autor { get; set; }

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
            Autor = new Autor();
        }
        else
        {
            Autor = await DB.Autori.FindAsync(IdAutora);
        }
        if (TempData["FormData"] is string formDataJson)
        {
            Autor = System.Text.Json.JsonSerializer.Deserialize<Autor>(formDataJson);
        }
    }
    public async Task<IActionResult> OnPostAsync() // uložit autora
    {
        if (!ModelState.IsValid)
        {
            string textChyby = System.Net.WebUtility.UrlEncode("Příjmení musí být vyplněno!!!");

            TempData["FormData"] = System.Text.Json.JsonSerializer.Serialize(Autor);
            return Redirect($"/edit/autor/0?Chyba={textChyby}");
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

            Autor.ImagePath = $"/uploads/autor/{fileName}";
        }

        if (IdAutora != Autor?.Id) { return BadRequest(); }

        if (IdAutora == 0)
        {
           await DB.Autori.AddAsync(Autor);
        }
        else
        {
            DB.Autori.Update(Autor);
        }
        // 1) Získat ID přihlášeného uživatele
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        // 2) Přiřadit ho k autorovi
        Autor.UserId = userId;

        await DB.SaveChangesAsync();
        return Redirect($"/autor/{Autor.Id}");
    }

    public async Task<IActionResult> OnPostVymazatAsync()
    {
        // ID si vezmeme přímo z vlastnosti modelu, nemusíme ho složitě parsovat z textu
        var autor = await DB.Autori.FindAsync(IdAutora);

        if (autor != null)
        {
            DB.Autori.Remove(autor);
            await DB.SaveChangesAsync();
        }

        return Redirect("/autori");
    }


}
