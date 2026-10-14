using Čtenářský_deník.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

namespace Čtenářský_deník.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        readonly ApplicationDbContext DB;

        public List<Autor> Autori { get; set; }

        public IndexModel(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            DB = db;
            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Autori = new List<Autor>();
            Autori = await DB.Autori
               .Where(x => x.UserId == userId)
               .OrderBy(x => x.Prijmeni)
               .ToListAsync();

        }
    }
}
