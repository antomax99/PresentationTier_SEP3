using Entities;

namespace Contracts;

public interface IProductService
{
    public Task<IList<Product>> GetProductsAsync();
    public Task<Product> GetProductById(int id);
    public Task AddProductAsync(Product product);
    public Task AddProductToCart(Product product, int orderId);
    public Task DeleteProductAsync(int id);
    public Task UpdateProductAsync(Product product);
}