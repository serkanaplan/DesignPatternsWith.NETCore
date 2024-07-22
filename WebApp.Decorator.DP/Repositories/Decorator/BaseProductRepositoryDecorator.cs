

using WebApp.Decorator.DP.Models;

namespace WebApp.Decorator.DP.Repositories.Decorator;

public class BaseProductRepositoryDecorator(IProductRepository productRepository) : IProductRepository
{
    public readonly IProductRepository _productRepository = productRepository;

    public virtual async Task<List<Product>> GetAll() => await _productRepository.GetAll();

    public virtual async Task<List<Product>> GetAll(string userId) => await _productRepository.GetAll(userId);

    public virtual async Task<Product> GetById(int id) => await _productRepository.GetById(id);

    public virtual async Task Remove(Product product) => await _productRepository.Remove(product);

    public virtual async Task<Product> Save(Product product) => await _productRepository.Save(product);

    public virtual async Task Update(Product product) => await _productRepository.Update(product);
}
