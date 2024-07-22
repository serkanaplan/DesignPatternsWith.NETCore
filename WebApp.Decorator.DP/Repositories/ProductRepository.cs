using Microsoft.EntityFrameworkCore;
using WebApp.Decorator.DP.Models;


namespace WebApp.Decorator.DP.Repositories;

public class ProductRepository(AppIdentityDbContext context) : IProductRepository
{
    private readonly AppIdentityDbContext _context = context;

    public async Task<List<Product>> GetAll() => await _context.Products.ToListAsync();

    public async Task<List<Product>> GetAll(string userId) => await _context.Products.Where(x => x.UserId == userId).ToListAsync();

    public async Task<Product> GetById(int id) => await _context.Products.FindAsync(id);

    public async Task Remove(Product product)
    {
        _context.Products.Remove(product);

        await _context.SaveChangesAsync();
    }

    public async Task<Product> Save(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task Update(Product product)
    {
        _context.Products.Update(product);

        await _context.SaveChangesAsync();
    }
}