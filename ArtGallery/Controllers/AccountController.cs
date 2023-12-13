using ArtGallery.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArtGallery.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                // Process registration logic, e.g., save to a database
                // For simplicity, we'll just return a success message
                return Content("Registration successful!");
            }

            // If ModelState is not valid, redisplay the registration form with validation errors
            return View(newUser);
        }
    }
}
