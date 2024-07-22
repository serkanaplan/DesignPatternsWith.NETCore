using WebApp.Observer.DP.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Observer.DP.Controllers;

public class AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : Controller
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly SignInManager<AppUser> _signInManager = signInManager;

    public IActionResult Login() => View();


    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        var hasUser = await _userManager.FindByEmailAsync(email);

        if (hasUser == null) return View();

        var signInResult = await _signInManager.PasswordSignInAsync(hasUser, password, true, false);

        if (!signInResult.Succeeded) return View();
        
        return RedirectToAction(nameof(HomeController.Index), "Home");
    }


    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction(nameof(HomeController.Index), "Home");
    }
    
}