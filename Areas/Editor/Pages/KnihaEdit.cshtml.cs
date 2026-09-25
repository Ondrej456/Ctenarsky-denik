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
        public Kniha Data { get; set; }

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
                Data = new Kniha();

                // pokud existují nějací autoři, nastav prvního
                
                     if (AutorId > 0)
                {
                    Data.AutorId = AutorId;

                    Autor = await DB.Autori
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == AutorId);
                }
               
            }
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


                // Pokud je ID > 0 → načítáme knihu z databáze
                Data = await DB.Knihy
                    .Include(x => x.Autor)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == IdKnihy);
                Autor = Data.Autor;
            }
                ObdobiSeznam = new SelectList(
                     await DB.ObdobiMaturita.ToListAsync(),
                    "Id",
                    "Nazev");


            if (TempData["FormData"] is string formDataJson)
            {
                Data = System.Text.Json.JsonSerializer.Deserialize<Kniha>(formDataJson);
            }

            await DB.SaveChangesAsync();
            
        }

        // Odeslání formuláře (POST)
        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Pokud formulář obsahuje chyby (Required, Range, atd.)
            ModelState.Remove("Data.UserId"); ModelState.Remove("Data.Autor");
            if (!ModelState.IsValid)
            {
                 
                // Pokud formulář obsahuje chyby (Required, Range, atd.)
                Autor = await DB.Autori
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.UserId == userId); ;

                TempData["FormData"] = System.Text.Json.JsonSerializer.Serialize(Data);

                string textChyby = System.Net.WebUtility.UrlEncode("Název musí být vyplněn!!!");

                return Redirect($"/edit/kniha/0?autorId={Data.AutorId}&Chyba={textChyby}"); // Vrátí zpět formulář 
            }
            // Ochrana: ID v URL musí odpovídat ID knihy z formuláře
            if (IdKnihy != Data?.Id) { return BadRequest(); } // Nesprávné ID → chyba

            // Pokud je ID 0 → vytváříme novou knihu
            if (IdKnihy == 0)
            {
                if (userId != null)
                {
                    Data.UserId = userId;
                }

                await DB.Knihy.AddAsync(Data);
            }
            else
            {
                if (userId != null)
                {
                    Data.UserId = userId;
                }
                // Jinak aktualizujeme existující knihu
                DB.Knihy.Update(Data);
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
                            BookId = Data.Id,
                            ImagePath = $"/uploads/knihy/{fileName}"
                        };

                        await DB.KnihaImages.AddAsync(img);
                    }
                }

                await DB.SaveChangesAsync();
            }

            // Přesměrování na detail autora (nebo kam chceš)
            return Redirect($"/autor/{Data.AutorId}");
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
    }
}
