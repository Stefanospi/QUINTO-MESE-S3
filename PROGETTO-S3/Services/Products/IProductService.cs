using PROGETTO_S3.Models;

namespace PROGETTO_S3.Services.Products
{
    public interface IProductService
    {
        public Task<Product> CreateProduct(Product product);
        public Task<List<Product>> GetAllProducts();
    }
}
