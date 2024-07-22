
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Decorator.DP.Models;
using WebApp.Decorator.DP.Repositories;

namespace WebApp.Decorator.DP.Controllers;


[Authorize]
public class ProductsController(IProductRepository productRepository) : Controller
{
    private readonly IProductRepository _productRepository = productRepository;


    public async Task<IActionResult> Index()
    {
        var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

        return View(await _productRepository.GetAll(userId));
    }


    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var product = await _productRepository.GetById(id.Value);
        
        if (product == null) return NotFound();

        return View(product);
    }


    public IActionResult Create() => View();


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Price,Stock")] Product product)
    {
        if (ModelState.IsValid)
        {
            var userId = User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            product.UserId = userId;
            await _productRepository.Save(product);
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }


    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var product = await _productRepository.GetById(id.Value);

        if (product == null) return NotFound();
        
        return View(product);
    }


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Stock,UserId")] Product product)
    {
        if (id != product.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                await _productRepository.Update(product);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(product.Id)) return NotFound();
                
                else throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }


    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var product = await _productRepository.GetById(id.Value);

        if (product == null) return NotFound();

        return View(product);
    }


    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var product = await _productRepository.GetById(id);

        await _productRepository.Remove(product);

        return RedirectToAction(nameof(Index));
    }


    private bool ProductExists(int id) => _productRepository.GetById(id) != null;
}
