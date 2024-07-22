using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApp.Adapter.DP.Models;
using WebApp.Adapter.DP.Services;

namespace WebApp.Adapter.DP.Controllers;

public class HomeController(ILogger<HomeController> logger,IImageProcess imageProcess) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    private readonly IImageProcess _imageProcess = imageProcess;


    public IActionResult Index() => View();

    public IActionResult Privacy() => View();

    public IActionResult AddWatermark() => View();

    [HttpPost]
    public async Task<IActionResult> AddWatermark(IFormFile image)
    {
        if (image is { Length: >= 0 })
        {
            var imageMemoryStream = new MemoryStream();

            await image.CopyToAsync(imageMemoryStream);

            _imageProcess.AddWatermark("Asp.Net Core MVC", image.FileName, imageMemoryStream);
        }
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
