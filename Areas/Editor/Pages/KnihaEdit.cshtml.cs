using Čtenářský_deník.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Čtenářský_deník.Areas.Editor.Pages
{
    public class KnihaEditModel : PageModel
    {
        // ID knihy z URL (SupportsGet = true umožní načítání přes GET)
        [BindProperty(SupportsGet = true)]
 
        public int IdKnihy { get; set; }

        // načte Id autora
        [BindProperty(SupportsGet = true)]
        public int AutorId { get; set; }

        // načte chybu - pokud je
        [BindProperty(SupportsGet = true)]
        public string Chyba { get; set; }

        // Data knihy z formuláře (POST)
        [BindProperty]
        public Kniha Kniha { get; set; }

        // Seznam autorů pro <select> v Razor stránce
        public Autor Autor { get; set; }

        // Přístup k databázi
        readonly ApplicationDbContext DB;

        public KnihaEditModel(ApplicationDbContext db)
        {
            DB = db;
        }

        [BindProperty]
        public List<IFormFile> Images { get; set; }

        // Načtení stránky (GET)

        public SelectList ObdobiSeznam { get; set; }
        public async Task OnGetAsync()
        {
            if (IdKnihy == 0)
            {
                Kniha = new Kniha();

                // pokud existují nějací autoři, nastav prvního
                
                     if (AutorId > 0)
                {
                    Kniha.AutorId = AutorId;

                    Autor = await DB.Autori
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == AutorId);
                }
               
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


                // Pokud je ID > 0 → načítáme knihu z databáze
                Kniha = await DB.Knihy
                    .Include(x => x.Autor)
                    .Include(x => x.Images)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == IdKnihy);
                Autor = Kniha.Autor;
            }
                ObdobiSeznam = new SelectList(
                     await DB.ObdobiMaturita.ToListAsync(),
                    "Id",
                    "Nazev");


            if (TempData["FormData"] is string formDataJson)
            {
                Kniha = System.Text.Json.JsonSerializer.Deserialize<Kniha>(formDataJson);
            }

            await DB.SaveChangesAsync();
            
        }

        // Odeslání formuláře (POST)
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Pokud formulář obsahuje chyby (Required, Range, atd.)
            ModelState.Remove("Kniha.UserId"); ModelState.Remove("Kniha.Autor");
            if (!ModelState.IsValid)
            {
                 
                // Pokud formulář obsahuje chyby (Required, Range, atd.)
                Autor = await DB.Autori
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.UserId == userId); ;

                TempData["FormData"] = System.Text.Json.JsonSerializer.Serialize(Kniha);

                string textChyby = System.Net.WebUtility.UrlEncode("Název musí být vyplněn!!!");

                return Redirect($"/edit/kniha/0?autorId={Kniha.AutorId}&Chyba={textChyby}"); // Vrátí zpět formulář 
            }

            if (IdKnihy == 0 && Kniha != null)
            {
                IdKnihy = Kniha.Id;
            }

            // Ochrana: ID v URL musí odpovídat ID knihy z formuláře
            if (IdKnihy != Kniha?.Id) { return BadRequest(); } // Nesprávné ID → chyba

            // Pokud je ID 0 → vytváříme novou knihu
            if (IdKnihy == 0)
            {
                if (userId != null)
                {
                    Kniha.UserId = userId;
                }

                await DB.Knihy.AddAsync(Kniha);
            }
            else
            {
                if (userId != null)
                {
                    Kniha.UserId = userId;
                }
                // Jinak aktualizujeme existující knihu
                DB.Knihy.Update(Kniha);
            }
            // Uloží změny do databáze
            await DB.SaveChangesAsync();
            // Ukládání obrázků
            if (Images != null && Images.Count > 0)
            {
                foreach (var file in Images)
                {
                    if (file.Length > 0)
                    {
                        var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
                        var filePath = Path.Combine(
                            Directory.GetCurrentDirectory(),
                                "wwwroot/uploads/knihy",
                                fileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        var img = new KnihaImage
                        {
                            BookId = Kniha.Id,
                            ImagePath = $"/uploads/knihy/{fileName}"
                        };

                        await DB.KnihaImages.AddAsync(img);
                    }
                }

                await DB.SaveChangesAsync();
            }

            // Přesměrování na detail autora (nebo kam chceš)
            return Redirect($"/kniha/{Kniha.Id}");
        }
        // smazání knihy
        public async Task<IActionResult> OnPostVymazatAsync()
        {
            // ID si vezmeme přímo z vlastnosti modelu, nemusíme ho složitě parsovat z textu
            var kniha = await DB.Knihy.FindAsync(IdKnihy);

            if (kniha != null)
            {
                DB.Knihy.Remove(kniha);
                await DB.SaveChangesAsync();
            }

            return Redirect("/knihy");
        }
        // mazání obrázků
        public async Task<IActionResult> OnPostSmazatObrazekAsync(int idObrazku)
        {
            // 1. Najdeme obrázek v databázi
            var obrazek = await DB.KnihaImages.FindAsync(idObrazku);

            if (obrazek != null)
            {
                // 2. Fyzické smazání souboru z disku (wwwroot)
                // Převedeme relativní cestu (/uploads/knihy/...) na absolutní cestu na disku
                var souborCesta = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", obrazek.ImagePath.TrimStart('/'));

                if (System.IO.File.Exists(souborCesta))
                {
                    System.IO.File.Delete(souborCesta);
                }

                // 3. Smazání záznamu z databáze
                DB.KnihaImages.Remove(obrazek);
                await DB.SaveChangesAsync();
            }

            // 4. Přesměrování zpět na aktuální stránku editace knihy, aby se změna ihned projevila
            return Redirect($"/edit/kniha/{IdKnihy}?autorId={AutorId}");
        }
    }
}
