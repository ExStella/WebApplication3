using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication3.Model;

namespace WebApplication3.Pages
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(ILogger<IndexModel> logger, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public UserDetailModel UserDetails { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                // Assuming you have a method to retrieve user details from your database
                UserDetails = GetUserDetails(user.Id);

                // Pass the user details to the view
                return Page();
            }

            return RedirectToPage("/Login"); // Redirect to login page if the user is not found
        }

        // You need to implement this method to retrieve user details from your database
        // Inside IndexModel class
        private UserDetailModel GetUserDetails(string userId)
        {
            try
            {
                // Implement logic to retrieve user details based on userId
                // For example, you might use a service or repository to fetch the details

                // Sample code (replace with your logic):
                var userDetails = new UserDetailModel
                {
                    // Populate user details here
                    // Example: FirstName = "John", LastName = "Doe", ...

                    // Fetch the claims from the user
                    FirstName = User.FindFirstValue(ClaimTypes.GivenName),
                    LastName = User.FindFirstValue(ClaimTypes.Surname),
                    Gender = User.FindFirstValue(ClaimTypes.Gender),
                    NRIC = User.FindFirstValue("NRIC"), // Replace with the actual claim type for NRIC
                    Email = User.FindFirstValue(ClaimTypes.Email),
                    DateOfBirth = DateTime.TryParse(User.FindFirstValue(ClaimTypes.DateOfBirth), out var dateOfBirth) ? dateOfBirth : DateTime.MinValue,
                    // Assuming you have a custom claim for ResumePath
                    Resume = User.FindFirstValue("ResumePath"),
                    WhoAmI = User.FindFirstValue("WhoAmI")
                };

                return userDetails;
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                _logger.LogError(ex, "Error in GetUserDetails");

                // You can also throw the exception for further investigation
                throw;
            }
        }
    }
}
