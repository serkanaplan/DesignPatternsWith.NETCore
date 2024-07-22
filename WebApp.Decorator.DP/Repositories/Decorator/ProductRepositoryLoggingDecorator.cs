
using WebApp.Decorator.DP.Models;

namespace WebApp.Decorator.DP.Repositories.Decorator;

public class ProductRepositoryLoggingDecorator(IProductRepository productRepository, ILogger<ProductRepositoryLoggingDecorator> log) : BaseProductRepositoryDecorator(productRepository)
{
    private readonly ILogger<ProductRepositoryLoggingDecorator> _log = log;

    public override Task<List<Product>> GetAll()
    {
        _log.LogInformation("GetAll methodu çalıştı");

        return base.GetAll();
    }

    public override Task<List<Product>> GetAll(string userId)
    {
        _log.LogInformation("GetAll(userId) methodu çalıştı");

        return base.GetAll(userId);
    }

    public override Task<Product> Save(Product product)
    {
        _log.LogInformation("Save methodu çalıştı");

        return base.Save(product);
    }

    public override Task Update(Product product)
    {
        _log.LogInformation("Update methodu çalıştı");

        return base.Update(product);
    }

    public override Task Remove(Product product)
    {
        _log.LogInformation("Remove methodu çalıştı");

        return base.Remove(product);
    }
}
