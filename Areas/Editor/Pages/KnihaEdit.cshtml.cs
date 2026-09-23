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

        // Data knihy z formuláře (POST)
        [BindProperty]
        public Kniha Data { get; set; }

        // Seznam autorů pro <select> v Razor stránce
        public List<Autor> Autori { get; set; }

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
                    var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                    Autori = await DB.Autori
                        .Where(x => x.UserId == userId)
                        .OrderBy(x => x.Prijmeni)
                        .ThenBy(x => x.Jmeno)
                        .AsNoTracking()
                        .ToListAsync();

                
                    if (Autori.Count > 0)
                    {
                        Data.AutorId = Autori[0].Id;
                    }
                }
            
            else
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                Autori = await DB.Autori
                    .Where(x => x.UserId == userId)
                    .OrderBy(x => x.Prijmeni)
                    .ThenBy(x => x.Jmeno)
                    .AsNoTracking()
                    .ToListAsync();

                // Pokud je ID > 0 → načítáme knihu z databáze
                Data = await DB.Knihy
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == IdKnihy);
            }
            ObdobiSeznam = new SelectList(
                await DB.ObdobiMaturita.ToListAsync(),
                "Id",
                "Nazev");

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
        }

        // Odeslání formuláře (POST)
        public async Task<IActionResult> OnPostAsync()
        {
            Console.WriteLine("Nahrané soubory: " + Images?.Count);
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // Pokud formulář obsahuje chyby (Required, Range, atd.)
            if (!ModelState.IsValid)
            {
                // Pokud formulář obsahuje chyby (Required, Range, atd.)
                Autori = await DB.Autori
                  .Where(x => x.UserId == userId)
                  .OrderBy(x => x.Prijmeni)
                  .ThenBy(x => x.Jmeno)
                  .AsNoTracking()
                  .ToListAsync();

                return Page(); // Vrátí zpět formulář s chybami
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
        public async Task<IActionResult> OnPostVymazatAsync(string idKnihy)
        {
            // 1) Ověření, že ID z URL odpovídá ID z formuláře
            if (IdKnihy != Convert.ToInt32(idKnihy))
            {
                return BadRequest();   // nesedí → chyba
            }
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // 2) Najdeme knihu podle ID
            var kniha = await DB.Knihy
                .FirstOrDefaultAsync(x => x.Id == IdKnihy && x.UserId == userId);

            if (kniha == null)
            {
                return NotFound();     // kniha neexistuje
            }

            // 3) Smažeme knihu

            DB.Knihy.Remove(kniha);
            await DB.SaveChangesAsync();
            return Redirect($"/knihy");
            
        }
    }
}
