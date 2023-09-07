using HealthAndMonetaryHarmony.Exceptions;
using HealthAndMonetaryHarmony.Services;
using Microsoft.AspNetCore.Mvc;
using PangeaCyber.Net.AuthN.Models;

namespace HealthAndMonetaryHarmony.Controllers
{
    public class AccountController : Controller
    {
        private readonly PangeaAuthService _authService;

        public AccountController(PangeaAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Register(string email, string password)
        {
            try
            {
                var response = await _authService.RegisterUser(email, password);
                // Handle successful registration, e.g., redirect to a confirmation page
                return RedirectToAction("RegistrationSuccess");
            }
            catch (UserAlreadyExistsException)
            {
                // Handle the case where the user already exists, e.g., show an error message
                ModelState.AddModelError("Email", "A user with this email already exists.");
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var response = await _authService.LoginUser(email, password);
                // Handle successful login, e.g., set authentication cookie and redirect to the dashboard
                return RedirectToAction("Dashboard");
            }
            catch (AuthenticationFailedException)
            {
                // Handle failed login, e.g., show an error message
                ModelState.AddModelError("Login", "Invalid email or password.");
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(string token, string oldPassword, string newPassword)
        {
            try
            {
                await _authService.UpdatePassword(token, oldPassword, newPassword);
                // Handle successful password update, e.g., show a success message or redirect
                return RedirectToAction("PasswordUpdateSuccess");
            }
            catch (PasswordPolicyViolationException)
            {
                // Handle password policy violation, e.g., show an error message
                ModelState.AddModelError("Password", "The new password does not meet the required policy.");
                return View();
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(string userId, Profile profile)
        {
            try
            {
                await _authService.UpdateProfile(userId, profile);
                // Handle successful profile update, e.g., show a success message or redirect
                return RedirectToAction("ProfileUpdateSuccess");
            }
            catch (UserNotFoundException)
            {
                // Handle the case where the user is not found, e.g., show an error message
                ModelState.AddModelError("User", "User not found.");
                return View();
            }
        }


        // You can continue to add more actions as needed based on the functionalities of PangeaAuthService
    }
}

