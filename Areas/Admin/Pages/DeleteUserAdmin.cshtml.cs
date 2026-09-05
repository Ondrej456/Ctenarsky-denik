using Čtenářský_deník.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Čtenářský_deník.Areas.Admin.Pages
{
    public class DeleteUserAdminModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext DB;

        public DeleteUserAdminModel(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            DB = db;
            _userManager = userManager;
        }

        public List<IdentityUser> Users { get; set; }
        public IdentityUser SelectedUser { get; set; }
        public List<Autor> Authors { get; set; }
        public List<Kniha> Books { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            Users = _userManager.Users.ToList();

            if (string.IsNullOrWhiteSpace(userId))
                return Page();

            SelectedUser = await _userManager.FindByIdAsync(userId);

            if (SelectedUser == null)
            {
                ErrorMessage = "Uživatel nebyl nalezen.";
                return Page();
            }

            Authors = await DB.Autori.Where(a => a.UserId == userId).ToListAsync();
            Books = await DB.Knihy.Where(k => k.UserId == userId).ToListAsync();

            return Page();
        }

       
    }
}
