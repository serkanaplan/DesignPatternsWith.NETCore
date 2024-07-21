﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Strategy.DP.Models;
using WebApp.Strategy.DP.Repo;

namespace WebApp.Strategy.DP.Controllers
{
    public class ProductsController(IProductRepository productRepository, UserManager<AppUser> userManager) : Controller
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly UserManager<AppUser> _userManager = userManager;


        public async Task<IActionResult> Index()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            return View(await _productRepository.GetAllByUserId(user.Id));
        }


        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        public IActionResult Create() => View();


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Stock,UserId,CreatedDate")] Product product)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                product.UserId = user.Id;
                product.CreatedDate = DateTime.Now;
                await _productRepository.Save(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }


        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Price,Stock,UserId,CreatedDate")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productRepository.Update(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }


        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var product = await _productRepository.GetById(id);


            await _productRepository.Delete(product);
            return RedirectToAction(nameof(Index));
        }


        private bool ProductExists(string id) => _productRepository.GetById(id) != null;
    }
}
