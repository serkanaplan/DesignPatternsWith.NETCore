using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.ChainOfResponsibility.DP.ChainOfResponsibility;
using WebApp.ChainOfResponsibility.DP.Models;

namespace WebApp.ChainOfResponsibility.DP.Controllers;

public class HomeController(ILogger<HomeController> logger, AppIdentityDbContext context) : Controller
{
    private readonly ILogger<HomeController> _logger = logger;
    private readonly AppIdentityDbContext _context = context;


    public IActionResult Index() => View();

    public async Task<IActionResult> SendEmail()
    {
        var products = await _context.Products.ToListAsync();

        var excelProcessHandler = new ExcelProcessHandler<Product>();

        var zipFileProcessHandler = new ZipFileProcessHandler<Product>();

        var sendEmailProcessHandler = new SendEmailProcessHandler("product.zip", "f-cakiroglu@outlook.com");

        excelProcessHandler.SetNext(zipFileProcessHandler).SetNext(sendEmailProcessHandler);

        excelProcessHandler.handle(products);

        return View(nameof(Index));
    }
    public IActionResult Privacy() => View();


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
