using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.ViewModels;
using IdentityUser = Microsoft.AspNetCore.Identity.IdentityUser;

namespace WebApplication3.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login LModel { get; set; }

        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;

        public LoginModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(LModel.Email);

                if (user != null && await userManager.IsLockedOutAsync(user))
                {
                    // User is locked out, display a specific error message
                    ModelState.AddModelError("", "User is locked out. Please try again later.");
                    return Page();
                }

                var result = await signInManager.PasswordSignInAsync(LModel.Email, LModel.Password,
                    LModel.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    return RedirectToPage("Index");
                }

                if (result.IsLockedOut)
                {
                    // User is locked out, display a specific error message
                    ModelState.AddModelError("", "User is locked out. Please try again later.");
                }
                else
                {
                    // Username or password incorrect
                    ModelState.AddModelError("", "Username or Password incorrect");

                    // Increment failed login attempts (if needed)
                    if (user != null)
                    {
                        await userManager.AccessFailedAsync(user);
                    }
                }
            }

            return Page();
        }
    }
}
