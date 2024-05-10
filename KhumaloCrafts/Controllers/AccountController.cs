using Logic_Layer.Interfaces;
using Logic_Layer.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Database_Layer.DatabaseEntities;

namespace View_layer.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly IUserRegistrationService _userRegistrationService;

        public AccountController(IUserRegistrationService userRegistrationService)
        {
            _userRegistrationService = userRegistrationService;
        }

        public IActionResult Register()
        {
            // Return the Register view
            return View("~/Views/Account/Register.cshtml");
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model); // Return the view with validation errors
            }

            // Call the user registration service to register the user
            var registrationResult = await _userRegistrationService.RegisterUserAsync(model.FirstName, model.LastName, model.Email, model.Password);

            if (registrationResult)
            {
                // Registration successful, redirect to login page
                return RedirectToAction(nameof(Login));
            }
            else
            {
                // Registration failed, add model error and return to registration form
                ModelState.AddModelError(string.Empty, "Registration failed or User already registered. Please try again.");
                return View(model);
            }
        }

        public IActionResult Login()
        {
            // Return the Login view
            return View("~/Views/Account/Login.cshtml");
        }

      
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Authenticate user here (e.g., check credentials against database)
                var isAuthenticated = await _userRegistrationService.AuthenticateUserAsync(model.Email, model.Password);

                if (isAuthenticated)
                {
                    // Retrieve the user's ID from the database
                    var userId = await _userRegistrationService.GetUserIdByEmailAsync(model.Email);

                    // Create claims identity for the authenticated user
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                // Add other claims as needed
            };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                    // Redirect user to the appropriate page (e.g., homepage)
                    return RedirectToAction("Transaction", "Transaction");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                }
            }

            // If authentication fails or model is invalid, return to login page with errors
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect user to the login page
            return RedirectToAction("Login", "Account");
        }
    }
}
