using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using YourProject.Models;

namespace YourProject.Controllers;

public class HomeController : Controller
{
    // Temporary in-memory storage for users
    private static List<UserModel> _users = new List<UserModel>();

    // Registration page (GET)
    public IActionResult Register()
    {
        return View();
    }

    // Handle registration form submission (POST)
    [HttpPost]
    public IActionResult RegisterUser(UserModel model)
    {
        if (ModelState.IsValid)
        {
            _users.Add(model);  // Store user in temporary memory
            return RedirectToAction("Login");  // Redirect to login page
        }
        return View("Register", model);  // If validation fails, return to registration form
    }

    // Login page (GET)
    public IActionResult Login()
    {
        return View();
    }

    // Handle login form submission (POST)
    [HttpPost]
    public IActionResult Authenticate(LoginModel model)
    {
        if (ModelState.IsValid)
        {
            // Check if user exists with matching email and password
            var user = _users.Find(u => u.Email == model.Email && u.Password == model.Password);
            if (user != null)
            {
                ViewBag.Message = "Login successful!";
                return View("Login");  // Redirect to Login page with success message
            }
            else
            {
                ModelState.AddModelError("", "Invalid email or password");
            }
        }
        return View("Login", model);  // If login fails, return to login form
    }
}