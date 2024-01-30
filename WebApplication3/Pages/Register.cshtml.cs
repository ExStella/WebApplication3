using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication3.ViewModels;
using System.Security.Claims;
using System.Threading.Tasks;
using System;

namespace WebApplication3.Pages
{
    public class RegisterModel : PageModel
    {
        private UserManager<IdentityUser> userManager { get; }
        private SignInManager<IdentityUser> signInManager { get; }

        [BindProperty]
        public Register RModel { get; set; }

        public RegisterModel(UserManager<IdentityUser> userManager,
                             SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = RModel.Email,
                    Email = RModel.Email
                };

                var result = await userManager.CreateAsync(user, RModel.Password);

                if (result.Succeeded)
                {
                    // Add claims for user properties
                    await userManager.AddClaimAsync(user, new Claim(ClaimTypes.GivenName, RModel.FirstName));
                    await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Surname, RModel.LastName));
                    await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Gender, RModel.Gender));
                    await userManager.AddClaimAsync(user, new Claim("NRIC", RModel.NRIC));
                    await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, RModel.Email));
                    await userManager.AddClaimAsync(user, new Claim(ClaimTypes.DateOfBirth, RModel.DateOfBirth.ToString("yyyy-MM-dd")));
                    await userManager.AddClaimAsync(user, new Claim("WhoAmI", RModel.WhoAmI));
                    // Add other claims

                    await signInManager.SignInAsync(user, false);
                    return RedirectToPage("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return Page();
        }
    }
}
