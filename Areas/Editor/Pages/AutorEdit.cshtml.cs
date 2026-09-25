using Čtenářský_deník.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;

namespace Čtenářský_deník.Areas.Editor.Pages;

public class AutorEditModel : PageModel
{
    [BindProperty(SupportsGet = true)]
    public int IdAutora { get; set; }

    [BindProperty(SupportsGet = true)]
    public string Chyba { get; set; }

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
        if (TempData["FormData"] is string formDataJson)
        {
            Data = System.Text.Json.JsonSerializer.Deserialize<Autor>(formDataJson);
        }
    }
    public async Task<IActionResult> OnPostAsync() // uložit autora
    {
        if (!ModelState.IsValid)
        {
            string textChyby = System.Net.WebUtility.UrlEncode("Příjmení musí být vyplněno!!!");

            TempData["FormData"] = System.Text.Json.JsonSerializer.Serialize(Data);
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
