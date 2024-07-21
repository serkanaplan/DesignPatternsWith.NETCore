using WebApp.Strategy.DP.Models;

namespace WebApp.Strategy.DP.Repo;

public interface IProductRepository
{
    Task<Product> GetById(string id);
    Task<List<Product>> GetAllByUserId(string userId);
    Task<Product> Save(Product product);
    Task Update(Product product);
    Task Delete(Product product);
}