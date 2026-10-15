using Čtenářský_deník.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;

namespace Čtenářský_deník.Areas.Admin.Pages
{
    public class DeleteUserAdminModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext DB;
        private readonly IWebHostEnvironment _environment; // služba co umožňuje najít cestu k souboru

        public DeleteUserAdminModel(ApplicationDbContext db, UserManager<IdentityUser> userManager, IWebHostEnvironment environment)
        {
            DB = db;
            _userManager = userManager;
            _environment = environment;
        }

        public List<IdentityUser> Users { get; set; }
        public IdentityUser SelectedUser { get; set; }
        [BindProperty]
        public string UserId { get; set; }
        public List<Autor> Authors { get; set; }
        public List<Kniha> Books { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId)
        {
            Users = _userManager.Users.ToList();
            UserId = userId;

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

        public async Task<IActionResult> OnPostVymazatAsync()
        {
            if (string.IsNullOrEmpty(UserId))
            {
                return Redirect("/Admin/DeleteUserAdmin");
            }
            var bookIds = await DB.Knihy
                    .Where(k => k.UserId == UserId)
                    .Select(k => k.Id)
                    .ToListAsync();

            var imagesKnihy = await DB.KnihaImages
                     .Where(i => bookIds.Contains(i.BookId))
                     .ToListAsync();


            foreach (var image in imagesKnihy)
            {
                var pathKO = Path.Combine(  // pathKO cesta k obrázku knihy
                    _environment.WebRootPath,
                    image.ImagePath.TrimStart('/'));

                if (System.IO.File.Exists(pathKO))
                {
                    System.IO.File.Delete(pathKO);
                }
            }

                 var autor = await DB.Autori
                        .FirstOrDefaultAsync(a => a.UserId == UserId);

                 var pathAO = Path.Combine(
                        _environment.WebRootPath,
                        autor.ImagePath.TrimStart('/'));

                if (System.IO.File.Exists(pathAO))
                {
                    System.IO.File.Delete(pathAO);
                }

            var user = await _userManager.FindByIdAsync(UserId);

            await _userManager.DeleteAsync(user);

            return Redirect("/Admin/DeleteUserAdmin");
        }
    }
}
