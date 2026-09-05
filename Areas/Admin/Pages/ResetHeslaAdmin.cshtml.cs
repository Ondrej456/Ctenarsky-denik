using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Čtenářský_deník.Areas.Admin.Pages 
{

    [BindProperties]
    public class ResetHeslaAdminModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ResetHeslaAdminModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        
        public string Nick { get; set; }
        public string NewPassword { get; set; }

       
        public string SuccessMessage { get; set; }

        
        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            ModelState.Clear();
            
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            if (string.IsNullOrWhiteSpace(Nick) || string.IsNullOrWhiteSpace(NewPassword))
            {
                ErrorMessage = "Vyplňte Nick i nové heslo.";
                return Page();
            }

            var user = await _userManager.FindByNameAsync(Nick);

            if (user == null)
            {
                ErrorMessage = "Uživatel s tímto Nickem neexistuje.";
                return Page();
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, NewPassword);

            if (result.Succeeded)
            {
                SuccessMessage = "Heslo bylo úspěšně resetováno.";
                ModelState.Clear();
                return Page();
            }

            ErrorMessage = string.Join(", ", result.Errors.Select(e => e.Description));
            return Page();
        }
    }
}
