using PROGETTO_S3.Models;

namespace PROGETTO_S3.Services.Products
{
    public interface IProductService
    {
        public Product CreateProduct(Product product);
        public List<Product> GetAllProducts();
    }
}
