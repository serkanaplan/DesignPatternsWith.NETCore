using MongoDB.Driver;
using WebApp.Strategy.DP.Models;

namespace WebApp.Strategy.DP.Repo;

public class ProductRepositoryFromMongoDb : IProductRepository
{
    private readonly IMongoCollection<Product> _productCollection;

    public ProductRepositoryFromMongoDb(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("MongoDb");

        var client = new MongoClient(connectionString);

        var database = client.GetDatabase("ProductDb");

        _productCollection = database.GetCollection<Product>("Products");
    }



    public async Task Delete(Product product) => await _productCollection.DeleteOneAsync(x => x.Id == product.Id);

    public async Task<List<Product>> GetAllByUserId(string userId) => await _productCollection.Find(x => x.UserId == userId).ToListAsync();

    public async Task<Product> GetById(string id) => await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
    
    public async Task Update(Product product) => await _productCollection.FindOneAndReplaceAsync(x => x.Id == product.Id, product);

    public async Task<Product> Save(Product product)
    {
        await _productCollection.InsertOneAsync(product);

        return product;
    }

}