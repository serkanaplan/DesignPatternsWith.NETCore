using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Template.DP.Models;

namespace WebApp.Template.DP.Controllers;

public class HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    private readonly UserManager<AppUser> _userManager = userManager;

    public async Task<IActionResult> Index() => View(await _userManager.Users.ToListAsync());

    public IActionResult Privacy() => View();


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
